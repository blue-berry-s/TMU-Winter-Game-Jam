using UnityEngine;

public class GameDifficultyLogic : MonoBehaviour
{
    public int currentDebt { get; private set; }
    private int currentLevel = 1;

    [SerializeField] private AnimationCurve DebtDifficulty;

    private void Start()
    {
        calcDifficulty();
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



}
