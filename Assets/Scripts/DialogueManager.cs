using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    private string[] BoughItemLines =
   {
    "A most profitable exchange, for you at least.",
    "May it serve you well… whilst you still breath.",
    "you art quickly becoming my most treasured patron."
};

    private string[] NotEnoughMoneyLines =
    {
    "Your purse rings hollow. Fortune favors you not this day.",
    "Return when thy coffers bear greater weight.",
    "Thinkest you I deal in charity? Nay."
};

    private string[] BeginningDialog =
    {
    "Word reaches mine ears that you are deep in debt to certain… powerful associates of mine.",
    "Perchance I might offer you salvation.",
    "Yet coin is never given freely in this house.",
    "I trust you hast studied the rules of Black Jack.",
    "For your very soul stands wagered upon the turn of cards.",
    "Mark this well…",
    "Surpass not twenty-one.",
    "Outmatch the dealer, shouldst you value your life.",
    "And once you doublest thy stake, but a single card shall fate grant you."
};

    private string[] LowHealthLines =
    {
    "you lookest pale… death’s breath lingers close upon your neck.",
    "Might I tempt you with one of mine exquisite elixirs? It restoreth flesh… for a modest price.",
    "Mayhap this game exceedeth your mortal limits."
};

    private string[] BoughtOrganLines =
    {
    "Best not inquire whence it came. Ignorance is a gentler burden.",
    "Mine eyes are ever turned the other way. Your secret is at peace in this house.",
    "May it lend you a most… helping hand."
};

    private string[] GeneralLines =
    {
    "Step forth, brave soul. Fortune awaiteth… and she is most fickle.",
    "All debts are paid in time: coin, blood, or otherwise.",
    "The cards speak truths that mortals scarce comprehend.",
    "Sit. Play. Let destiny unfold as it must.",
    "Every wager bringeth you closer to redemption… or ruin."
};



    public TMP_Text dialogueText;
    private string[] Currentlines;
    public float textSpeed;

    public GameObject dialogueCanvas;

    private int Index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        dialogueText.text = string.Empty;
        if (FindFirstObjectByType<CoreManager>().firstPlaythrough)
        {
            FindFirstObjectByType<CoreManager>().seenTutorial();
            readTutorital();
        }
        else
        {
            if (FindFirstObjectByType<HealthManager>().getPlayerHealth() <= 10)
            {
                PickRandom(LowHealthLines);
            }
            else
            {
                PickRandom(GeneralLines);
            }

        }


    }

    private void setActiveCanvas(bool active) {
        dialogueText.gameObject.SetActive(active);
        dialogueCanvas.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == Currentlines[Index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = Currentlines[Index];
            }
        }
    }

    void StartDialog()
    {
        Index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in Currentlines[Index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (Index < Currentlines.Length - 1)
        {
            Index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            setActiveCanvas(false);
        }
    }

    public void NotEnoughMoney()
    {
        PickRandom(NotEnoughMoneyLines);
    }

    public void BoughtItem()
    {
        PickRandom(BoughItemLines);
    }

    public void readTutorital()
    {
        Currentlines = BeginningDialog;
        Index = 0;
        dialogueText.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeLine());

        setActiveCanvas(true);
    }

    public void PickRandom(string[] dialogList)
    {
        int rand = Random.Range(0, dialogList.Length);

        Currentlines = new string[] { dialogList[rand] };

        Index = 0;
        dialogueText.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeLine());

        setActiveCanvas(true);
    }

    public void BoughtOrgan()
    {
        PickRandom(BoughtOrganLines);
    }
}
