using UnityEngine;

public class GameDifficultyLogic : MonoBehaviour
{
    public int currentDebt { get; private set; }
    private int currentLevel = 1;

    [SerializeField] private AnimationCurve DebtDifficulty;
    public bool cannotSkip { get; private set; }
    public bool wonGame { get; private set; }

    public int maxLevel = 7;

    private void Start()
    {
        wonGame = false;
        calcDifficulty();
        cannotSkip = false;
    }

    private void Update()
    {
        
        if (currentLevel > maxLevel && !(wonGame)) {
            Debug.Log("HERE");
            wonGame = true;
            winGame();
        }
    }

    public bool nextLevel() {
        currentLevel++;
        if (currentLevel > maxLevel)
        {
            return false;
        }
        else {
            return true;
        }

    }
    public void calcDifficulty() {
        currentDebt = Mathf.RoundToInt(DebtDifficulty.Evaluate(currentLevel));
    }

    public int getDebt() {
        calcDifficulty();
        return currentDebt;
    }

    public int getCurrentLevel() {
        return currentLevel;
    }

    public void triggerCannotSkip() {
        cannotSkip = true;
    }

    public void stopCannotSkip() {
        cannotSkip = true;
    }

    public void winGame() {
        //Debug.Log("HERE B");
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.GoodEnd, setActive: true)
            .withOverlay()
            .withClearUnusedAssets()
            .Perform();
    }

    public void resetWin() {
        wonGame = false;
    }



}