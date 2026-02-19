using UnityEngine;
using TMPro;

public class BadEndManager : MonoBehaviour
{
    private GameDifficultyLogic gameDifficulty;
    [SerializeField] private TMP_Text levelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameDifficulty = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<GameDifficultyLogic>();
        levelText.text = "You got to level " + gameDifficulty.getCurrentLevel().ToString();
    }

    public void restartGame() {
        FindFirstObjectByType<SoundManager>().playUIButton();
        SceneController.Instance
            .newTransition()
            .unload(SceneDatabse.Scenes.BadEnd)
            .unload(SceneDatabse.Slots.Session)
            .load(SceneDatabse.Slots.Session, SceneDatabse.Scenes.Session)
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.Shop, setActive: true)
            .withClearUnusedAssets()
            .Perform();
    }

}
