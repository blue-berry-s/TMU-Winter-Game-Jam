using UnityEngine;

public class CoreManager : MonoBehaviour
{

    public bool firstPlaythrough { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstPlaythrough = true;
        SceneController.Instance
                .newTransition()
                .load(SceneDatabse.Slots.Menu, SceneDatabse.Scenes.MainMenu)
                .Perform();
    }

    public void seenTutorial() {
        firstPlaythrough = false;
    }

    public void resetTutorial() {
        firstPlaythrough = true;
    }




    
}
