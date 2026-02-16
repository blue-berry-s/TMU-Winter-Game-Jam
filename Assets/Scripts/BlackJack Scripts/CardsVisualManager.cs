using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;


public class CardsVisualManager : MonoBehaviour
{

    [SerializeField] private CardView cardView;


    [SerializeField] private Transform discardPoint;
    [SerializeField] private Transform spawnPoint;

    public GameObject spawnCard(bool flipped, Card drawnCard)
    {
            CardView view = Instantiate(cardView, spawnPoint.position, spawnPoint.rotation);
            view.Setup(drawnCard, flipped);
            return view.gameObject;  
    }

    public void DrawCardAnimation(List<GameObject> handCardCount, int maxHandSize, SplineContainer splineContainer)
    {
        if (handCardCount.Count == 0) return;
        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = 0.5f - (handCardCount.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCardCount.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            handCardCount[i].transform.DOMove(splinePosition, 0.25f);
            handCardCount[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);


        }
    }

    public IEnumerator DiscardDeckAnimation(List<GameObject> handCardCount)
    {
        for (int i = 0; i < handCardCount.Count; i++)
        {
            handCardCount[i].transform.DOMove(discardPoint.position, 0.2f);
            handCardCount[i].transform.DOLocalRotateQuaternion(discardPoint.rotation, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DiscardCardAnimation(GameObject card, float visualTime)
    {
        card.transform.DOMove(discardPoint.position, visualTime);
        card.transform.DOLocalRotateQuaternion(discardPoint.rotation, visualTime);
    }

    public void MoveCardAnimation(GameObject card, Transform position,float visualTime)
    {
        card.transform.DOMove(position.position, visualTime);
        card.transform.DOLocalRotateQuaternion(position.rotation, visualTime);
    }


}
