using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public GameObject goSendMessage;

    public string MessageToSend;

    public Color ColorPressDown, ColorPressUp;

    public void OnPointerClick(PointerEventData PED)
    {
        if (goSendMessage)
        {
            goSendMessage.SendMessage(MessageToSend);
        }
    }

    public void OnPointerDown(PointerEventData PED)
    {
        var img = GetComponent<Image>();

        img.color = ColorPressDown;
    }

    public void OnPointerUp(PointerEventData PED)
    {
        var img = GetComponent<Image>();

        img.color = ColorPressUp;
    }
}
