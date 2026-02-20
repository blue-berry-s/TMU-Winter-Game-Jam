using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{




    public string[] BoughItemLines = { "Pleasure doing business", "I hope it serves you well", "You're my favourite customer" };
    public string[] NotEnoughMoneyLines = {"Looks like you don't have quite enough",  "Come back when you have more coins to spend", "No Chairty from me today"};
    public string[] BeginningDialog = { "Looks like you owe quite some debt to some serious people", "I know some people who can help", "But you won't get the money for free", "I hope you brushed up on the rules of Black Jack...", "Because your life is on the line" };


    public TMP_Text dialogueText;
    public string[] Currentlines;
    public float textSpeed;

    private int Index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        FindFirstObjectByType<DialogueManager>().readTutorital();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (dialogueText.text == Currentlines[Index])
            {
                NextLine();
            }
            else {
                StopAllCoroutines();
                dialogueText.text = Currentlines[Index];
            }
        }
    }

    void StartDialog() {
        Index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        foreach (char c in Currentlines[Index].ToCharArray()) {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (Index < Currentlines.Length - 1)
        {
            Index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else {
            gameObject.SetActive(false);
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

    public void readTutorital() {
        Currentlines = BeginningDialog;
        Index = 0;
        dialogueText.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeLine());

        gameObject.SetActive(true);
    }

    public void PickRandom(string[] dialogList)
    {
        int rand = Random.Range(0, dialogList.Length);

        Currentlines = new string[] { dialogList[rand] };

        Index = 0;
        dialogueText.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeLine());

        gameObject.SetActive(true);
    }






}
