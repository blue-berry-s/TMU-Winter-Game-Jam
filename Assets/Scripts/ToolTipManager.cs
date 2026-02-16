using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ToolTipManager : MonoBehaviour
{
    public Camera cam;
    public TMP_Text ToolTipText;
    public int xOffset = 10;
    public int yOffset = 5;
    #region singleton

    public static ToolTipManager _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    #endregion singleton
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 screenPoint = new Vector3(mouseScreenPos.x + xOffset, mouseScreenPos.y + yOffset, 10f);
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPoint);
        transform.position = screenPoint;
    }

    public void SetAndShowToolTip(string message) {
        gameObject.SetActive(true);
        ToolTipText.text = message;
    }

    public void HideToolTip() {
        gameObject.SetActive(false);
        ToolTipText.text = string.Empty;
    }
}
