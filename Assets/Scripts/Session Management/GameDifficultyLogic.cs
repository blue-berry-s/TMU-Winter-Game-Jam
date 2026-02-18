using UnityEngine;

public class GameDifficultyLogic : MonoBehaviour
{
    public int currentDebt { get; private set; }
    private int currentLevel = 1;

    [SerializeField] private AnimationCurve DebtDifficulty;
    public bool cannotSkip { get; private set; }

    private void Start()
    {
        calcDifficulty();
        cannotSkip = false;
    }

    public void nextLevel() {
        currentLevel++;

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



}
