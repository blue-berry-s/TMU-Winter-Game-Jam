using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void startSession() {
        FindFirstObjectByType<SoundManager>().playUIButton();
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.Session, SceneDatabse.Scenes.Session)
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.Shop, setActive: true)
            .unload(SceneDatabse.Slots.Menu)
            .withOverlay()
            .withClearUnusedAssets()
            .Perform();

    }
}
