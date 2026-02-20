using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenDeckDisplay : MonoBehaviour
{
    [SerializeField] private GameObject DeckDisplay;

    //UI Elements
    public float hoverScaleMultiplier = 1.2f;
    public float scaleSpeed = 5f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    void Awake()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }
    private void OnMouseDown()
    {
        DeckDisplay.SetActive(true);
    }

    private void OnEnable()
    {

        DeckDisplay.SetActive(false);
    }


    private void OnMouseEnter()
    {

        targetScale = originalScale * hoverScaleMultiplier;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.80f, 0.80f, 0.80f);
        
    }

    private void OnMouseExit()
    {
        targetScale = originalScale;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
        );
    }

}
