using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public TMP_Text ValueText;
    public Slider SliderUI;
    private byte bValue;

    public void UpdateText()
    {
        bValue = (byte)SliderUI.value;
        ValueText.text = bValue.ToString("00");
    }

    public void ResetValue()
    {
        bValue = 1;
        SliderUI.value = bValue;
        ValueText.text = bValue.ToString("00");
    }

    public byte GetIntValue()
    {
        return bValue;
    }

    public void SendValue()
    {
        var animWheel = FindObjectOfType<WheelAnimScript>();

        animWheel.SetSynthValue(bValue);
    }
}
