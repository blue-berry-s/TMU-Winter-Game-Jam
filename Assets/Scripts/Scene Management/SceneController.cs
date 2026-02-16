using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    #region SingleTon

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion  SingleTon

    private Dictionary<string, string> loadSceneBySlot = new();
    private bool isBusy = false;
    [SerializeField] private LoadingOverlay loadingOverlay;
    

    public SceneTransitionPlan newTransition() {
        return new SceneTransitionPlan();
    }

    private Coroutine ExecutePlan(SceneTransitionPlan plan) {
        if (isBusy) {
            Debug.LogWarning("Scene change is already in progress");
            return null;
        }
        isBusy = true;
        return StartCoroutine(ChangeSceneRoutine(plan));
    }

    private IEnumerator ChangeSceneRoutine(SceneTransitionPlan plan) {
        if (plan.Overlay) {
            yield return loadingOverlay.FadeInBlack();
            yield return new WaitForSeconds(0.5f);
        }
        foreach (var slotKey in plan.ScenesToUnload) {
            yield return UnloadSceneRoutine(slotKey);
        }
        if (plan.ClearUnusedAssets) yield return CleanupUnusedAssetsRoutine();

        foreach (var kvp in plan.ScenesToLoad) {
            if (loadSceneBySlot.ContainsKey(kvp.Key)) {
                yield return UnloadSceneRoutine(kvp.Key);
            }
            yield return LoadAdditiveRoutine(kvp.Key, kvp.Value, plan.ActiveSceneName == kvp.Value);
        }
        if (plan.Overlay) {
            yield return loadingOverlay.FadeOutBlack();
        }
        isBusy = false;
    }

    private IEnumerator LoadAdditiveRoutine(string slotKey, string sceneName, bool setActive) {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (loadOp == null) yield break;
        loadOp.allowSceneActivation = false;
        while (loadOp.progress < 0.9f) {
            yield return null;
        }
        loadOp.allowSceneActivation = true;
        while (!loadOp.isDone) {
            yield return null;
        }
        if (setActive) {
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            if (newScene.IsValid() && newScene.isLoaded) {
                SceneManager.SetActiveScene(newScene);
            }
        }
        loadSceneBySlot[slotKey] = sceneName;
    }

    private IEnumerator UnloadSceneRoutine(string slotKey) {
        if (!loadSceneBySlot.TryGetValue(slotKey, out string sceneName)) yield break;
        if (string.IsNullOrEmpty(sceneName)) yield break;
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneName);
        if (unloadOp != null) {
            while (!unloadOp.isDone) {
                yield return null;
            }
            
        }
        loadSceneBySlot.Remove(slotKey);
    }

    private IEnumerator CleanupUnusedAssetsRoutine() {
        AsyncOperation cleanupOp = Resources.UnloadUnusedAssets();
        while (!cleanupOp.isDone) {
            yield return null;
        }
    }

    public class SceneTransitionPlan {
        public Dictionary<string, string> ScenesToLoad { get; } = new() ;
        public List<string> ScenesToUnload { get; } = new();
        public string ActiveSceneName { get; private set; } = "";
        public bool ClearUnusedAssets { get; private set; } = false;
        public bool Overlay { get; private set; } = false;

        public SceneTransitionPlan load(string slotKey, string sceneName, bool setActive = false) {
            ScenesToLoad[slotKey] = sceneName;
            if (setActive) ActiveSceneName = sceneName;
            return this;
        }

        public SceneTransitionPlan unload(string slotKey)
        {
            ScenesToUnload.Add(slotKey);
            return this;
        }

        public SceneTransitionPlan withOverlay() {
            Overlay = true;
            return this;
        }

        public SceneTransitionPlan withClearUnusedAssets() {
            ClearUnusedAssets = true;
            return this;
        }

        public Coroutine Perform() { 
            return SceneController.Instance.ExecutePlan(this);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void nextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
