using UnityEngine;
using TMPro;

public class GoodEndManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;

    private void Start()
    {
        coinText.text = FindFirstObjectByType<MoneyManager>().getPlayerMoney().ToString();
    }

    public void restartGame()
    {
        FindFirstObjectByType<CoreManager>().resetTutorial();
        FindFirstObjectByType<GameDifficultyLogic>().resetWin();
        FindFirstObjectByType<SoundManager>().restartSoundtrack();
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
