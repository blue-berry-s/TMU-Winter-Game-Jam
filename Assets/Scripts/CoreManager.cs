using UnityEngine;

public class CoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneController.Instance
                .newTransition()
                .load(SceneDatabse.Slots.Menu, SceneDatabse.Scenes.MainMenu)
                .Perform();
    }

    
}
