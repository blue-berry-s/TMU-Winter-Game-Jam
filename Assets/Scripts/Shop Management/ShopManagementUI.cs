using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;


public class ShopManagementUI : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moneyText;

    private HealthManager healthManager;
    private MoneyManager moneyManager;
    private GameDifficultyLogic difficulty;

    public Transform shopGrid;

    [SerializeField] GameObject shopItemPrefab;

    [SerializeField] Button NextDebtButton;
    [SerializeField] GameObject skipButton;
    [SerializeField] GameObject skipText;

    [SerializeField] private TMP_Text RerollButton;



    private void Start()
    {
        healthManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<HealthManager>();
        moneyManager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<MoneyManager>();
        difficulty = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<GameDifficultyLogic>();
        NextDebtButton.GetComponentInChildren<TMP_Text>().text = "Next Debt: $" + difficulty.getDebt();

    }

    public void updateRerollButton(int cost) {
        RerollButton.text = "Reroll: $" + cost;
    }


  

    public void updateTexts() {
        healthText.text = healthManager.getPlayerHealth().ToString();
        moneyText.text = moneyManager.getPlayerMoney().ToString();
    }

    public void displayShop(List<ShopItem> itemsForSale) {
        foreach (Transform child in shopGrid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (ShopItem i in itemsForSale) {
            ShopItemView view = Instantiate(shopItemPrefab, shopGrid).GetComponent<ShopItemView>();
            view.setUpBuy(i);
        }
    }

    private void Update()
    {
        if (moneyManager.getPlayerMoney() >= difficulty.getDebt())
        {
            skipButton.SetActive(true);
            skipText.SetActive(true);
            NextDebtButton.onClick.RemoveAllListeners();
            NextDebtButton.onClick.AddListener(switchToForceGame);
        }
        else {
            skipButton.SetActive(false);
            skipText.SetActive(false);
            NextDebtButton.onClick.RemoveAllListeners();
            NextDebtButton.onClick.AddListener(switchToGame);
        }
    }



    public void switchToGame()
    {
        FindFirstObjectByType<SoundManager>().playUIButton();
        SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.BlackJack, setActive: true)
            .withOverlay()
            .Perform();
    }

    public void switchToForceGame()
    {
        
        difficulty.triggerCannotSkip();
        switchToGame();
    }

    public void callSkipLevel()
    {
        FindFirstObjectByType<SoundManager>().playUIButton();
        StartCoroutine(skipNextLevel());
    }

    private IEnumerator skipNextLevel() {

        bool checkWin = difficulty.nextLevel();
        if (checkWin)
        {
            SceneController.Instance
            .newTransition()
            .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.Shop, setActive: false)
            .withOverlay()
            .Perform();


            SceneController.Instance
                .newTransition()
                .load(SceneDatabse.Slots.SessionContent, SceneDatabse.Scenes.Shop, setActive: true)
                .withOverlay()
                .Perform();
            yield return new WaitForSeconds(0.2f);
            moneyManager.decPlayerMoney(difficulty.getDebt());
        }
        else {
            moneyManager.decPlayerMoney(difficulty.getDebt());
        }
        
        

    }
}
