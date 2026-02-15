using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void switchToGame() {
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.BlackJack, setActive: true)
            .withOverlay()
            .Perform();
    }
}
