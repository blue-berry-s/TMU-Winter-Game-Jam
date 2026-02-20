using UnityEngine;

public class DebtManager : MonoBehaviour
{

    private GameDifficultyLogic gameDifficulty;
    private MoneyManager moneyManager;

    private int currentRound = 1;
    [SerializeField] private int maxRounds = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameDifficulty = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<GameDifficultyLogic>();
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        currentRound = 1;

    }

    public bool nextRound() {
        if (currentRound + 1 > maxRounds)
        {
            endWave();
            return false;
        }
        else {
            if (playerWonLoss() == 1 && !gameDifficulty.cannotSkip) {
                endWave();
                return false;
            }
            currentRound++;
            return true;
        }

    }

    public int playerWonLoss() {
        if (moneyManager.getPlayerMoney() < gameDifficulty.currentDebt)
        {
            return -1;
        }
        else if (moneyManager.getPlayerMoney() >= gameDifficulty.currentDebt)
        {
            return 1;
        }

        return 0;
    }

    public void endWave() {
        currentRound = 1;
        if (moneyManager.getPlayerMoney() < gameDifficulty.currentDebt)
        {
            FindFirstObjectByType<SoundManager>().stopSoundtrack();
            FindFirstObjectByType<SoundManager>().playPlayerDie();
            switchToLost();
            Debug.Log("PLAYER LOST");
        }
        else if (!gameDifficulty.cannotSkip && moneyManager.getPlayerMoney() >= gameDifficulty.currentDebt)
        {
            moneyManager.decPlayerMoney(gameDifficulty.currentDebt);
            bool checkWin = gameDifficulty.nextLevel();
            if (checkWin) {
                switchToShop();
            }
            
        }
        else if (gameDifficulty.cannotSkip && moneyManager.getPlayerMoney() >= gameDifficulty.currentDebt)
        {
            moneyManager.decPlayerMoney(gameDifficulty.currentDebt);
            bool checkWin = gameDifficulty.nextLevel();
            if (checkWin) {
                switchToShop();
            }
            
        }
        else {
            Debug.Log("ERROR");
        }
        gameDifficulty.stopCannotSkip();
    }

    public void switchToShop()
    {
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.BlackJack, setActive: false)
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.Shop, setActive: true)
            .withOverlay()
            .withClearUnusedAssets()
            .Perform();
    }

    public int getMaxRound() {
        return maxRounds;
    }

    public int getCurrentRound() {
        return currentRound;
    }

    public int getCurrentDebt() {
        return gameDifficulty.getDebt();
    }

    public void switchToLost() {
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.BlackJack, setActive: false)
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.BadEnd, setActive: true)
            .withOverlay()
            .withClearUnusedAssets()
            .Perform();
    }

}
