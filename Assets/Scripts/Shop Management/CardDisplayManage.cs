using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CardDisplayManage : MonoBehaviour
{
    [SerializeField] private SplineContainer heartsSpline;
    [SerializeField] private SplineContainer clubsSpline;
    [SerializeField] private SplineContainer diamondsSpline;
    [SerializeField] private SplineContainer spadesSpline;

    private List<GameObject> clubsDisplay = new();
    private List<GameObject> heartsDisplay = new();
    private List<GameObject> diamondsDisplay = new();
    private List<GameObject> spadesDisplay = new();

    [SerializeField] private int maxPerSuit = 20;

    [SerializeField] Transform spawnPoint;
    [SerializeField] private CardView cardView;

    private CardManager cardmanager;

    private void Start()
    {
        cardmanager = GameObject.FindGameObjectWithTag("SessionManagers").GetComponentInChildren<CardManager>();
        cardmanager.setUpDeck();
        StartCoroutine(showFullDeck());
    }
    IEnumerator showFullDeck()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(displayCards());
    }

    IEnumerator displayCards()
    {
        StartCoroutine(MoveCards(clubsDisplay, cardmanager.getPlayerClubs(), clubsSpline)); ;
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(MoveCards(heartsDisplay, cardmanager.getPlayerHearts(), heartsSpline));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(MoveCards(spadesDisplay, cardmanager.getPlayerSpades(), spadesSpline));
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(MoveCards(diamondsDisplay, cardmanager.getPlayerDiamonds(), diamondsSpline));
    }
    IEnumerator MoveCards(List<GameObject> display, List<Card> cards, SplineContainer splineContainer)
    {

        for (int i = 0; i < cards.Count; i++)
        {
            CardView view = Instantiate(cardView, spawnPoint.position, spawnPoint.rotation);
            view.Setup(cards[i]);
            display.Add(view.gameObject);

        }
        if (cards.Count == 0) yield return new WaitForSeconds(0.15f);
        float cardSpacing = 1f / maxPerSuit;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            display[i].transform.DOMove(splinePosition, 0.15f);
            display[i].transform.DOLocalRotateQuaternion(rotation, 0.15f);
            yield return new WaitForSeconds(0.12f);

        }

    }

    public void clearDeck()
    {
        for (int i = 0; i < clubsDisplay.Count; i++)
        {
            Destroy(clubsDisplay[i].gameObject);
        }
        for (int i = 0; i < heartsDisplay.Count; i++)
        {
            Destroy(heartsDisplay[i].gameObject);
        }
        for (int i = 0; i < diamondsDisplay.Count; i++)
        {
            Destroy(diamondsDisplay[i].gameObject);
        }
        for (int i = 0; i < spadesDisplay.Count; i++)
        {
            Destroy(spadesDisplay[i].gameObject);
        }

        clubsDisplay = new();
        heartsDisplay = new();
        diamondsDisplay = new();
        spadesDisplay = new();
    }
}
