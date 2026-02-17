using System.Collections;
using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public RectTransform sideMenuRectTransform;
    private float width;
    private float startPositionX;
    private float startingAnchoredPositionX;

    public enum Side { left, right }
    public Side side;

    // Start is called before the first frame update
    void Start()
    {
        width = sideMenuRectTransform.rect.width;
    }


    private float GetMinPosition()
    {
        if (side == Side.left)
            return -width;   // fully hidden left
        else
            return width;    // fully hidden right
    }

    private float GetMaxPosition()
    {
        return 0f;           // fully open
    }

    private IEnumerator HandleMenuSlide(float slideTime, float startingX, float targetX)
    {
        float elapsed = 0f;

        while (elapsed < slideTime)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsed / slideTime);
            float newX = Mathf.Lerp(startingX, targetX, t);

            sideMenuRectTransform.anchoredPosition =
                new Vector2(newX, 0);

            yield return null;
        }
        sideMenuRectTransform.anchoredPosition =
            new Vector2(targetX, 0);
    }

    public void callOpenMenu () {
        StartCoroutine(OpenMenu());
    }

    public void callCloseMenu() {
        StartCoroutine(CloseMenu());
    }

    IEnumerator OpenMenu()
    {
        StopAllCoroutines();
        yield return StartCoroutine(HandleMenuSlide(
            .25f,
            sideMenuRectTransform.anchoredPosition.x,
            GetMaxPosition()
        ));
    }


    IEnumerator CloseMenu()
    {
        StopAllCoroutines();
        yield return StartCoroutine(HandleMenuSlide(
            .25f,
            sideMenuRectTransform.anchoredPosition.x,
            GetMinPosition()
        ));
    }







}