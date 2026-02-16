using UnityEngine;

public class GameDifficultyLogic : MonoBehaviour
{
    private int currentDebt;
    private int currentLevel = 1;

    [SerializeField] private int startingDebt = 10;

    [SerializeField] private int maxNumberRound = 5;
    int currentRound = 1;

    public void nextLevel() {
        currentLevel++;

    }
    public void increaseDifficulty() { 
        
    }

}
