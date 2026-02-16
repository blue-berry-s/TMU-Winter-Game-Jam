using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BodyPartToolTip : MonoBehaviour
{
    void Update()
    {
        if (EventSystem.current == null || Mouse.current == null)
            return;

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {

            GameObject hoveredObject = results[0].gameObject;
            if (hoveredObject.name == "Image") {
                BodyPartView bodyPart = hoveredObject.GetComponent<RectTransform>().parent.GetComponent<BodyPartView>();
                if (bodyPart != null)
                {
                    var msg = string.Format("{0}\nValue: {1}\nHealth: {2}", bodyPart.getName(), bodyPart.getMoney(), bodyPart.getHealth());
                    ToolTipManager._instance.SetAndShowToolTip(msg);
                }
                else
                {
                    ToolTipManager._instance.HideToolTip();
                }

            }


        }
        else {
            ToolTipManager._instance.HideToolTip();
        }
    }
}
