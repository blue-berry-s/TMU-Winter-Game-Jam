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

}
