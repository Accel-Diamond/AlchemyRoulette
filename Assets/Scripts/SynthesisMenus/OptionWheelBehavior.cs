using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OptionWheelBehavior : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image[] SynthRenders;

    public Animator MainAnim;

    public TMP_Text TextBox;

    public GameObject[] WindowsArray;

    public Color ColorPressDown, ColorPressUp;

    public RectTransform[] OptionsWheelT;

    public byte intStep;

    public Image[] imgRaycasts;

    public Image TopMask, LeftMask, TopSMask, LeftSMask;

    private bool DragTurning;

    private float tTurnTime;

    private byte intPos;

    private Vector3 PreWheelPos, PostWheelPos;

    public float fltRotThreshold;

    private float fltRot, fltPEDDif;

    private Vector2 PEDPos_O, PEDPos_F;

    private void Start()
    {
        var imgCast = GetComponent<Image>();

        if (imgCast)
        {
            imgCast.raycastTarget = false;
        }

        PreWheelPos = OptionsWheelT[intStep].eulerAngles;

        PostWheelPos = PreWheelPos;
    }

    // Proves automatic rotation to selected step
    public void Update()
    {
        if (!DragTurning)
        {
            if (tTurnTime < 1f)
            {
                tTurnTime += Time.deltaTime * 3f;

                if (OptionsWheelT[intStep])
                {
                    OptionsWheelT[intStep].eulerAngles = Vector3.Lerp(PreWheelPos, PostWheelPos, tTurnTime);
                }
            }
            else
            {
                if (OptionsWheelT[intStep])
                {
                    PreWheelPos = OptionsWheelT[intStep].eulerAngles;
                }

                DragTurning = true;

                UpdateTextBox();

                TurnOnRaycasts();
            }
        }
    }

    public void UpdateTextBox()
    {
        if (intStep == 0)
        {
            switch (intPos)
            {
                case 0:
                    TextBox.text = "Use materials to create new items.";
                    break;
                case 1:
                    TextBox.text = "Level Up your Synthesis Roulette to create more Items.";

                    TurnOnTop();
                    TurnOnLeft();
                    break;
                case 2:
                    TextBox.text = "Review what materials you have available.";

                    TurnOnTop();
                    TurnOnLeft();
                    break;
                case 3:
                    TextBox.text = "Check the recipes you've collected and crafted.";
                    break;
            }
        }
        else
        {
            switch (intPos)
            {
                case 0:
                    TextBox.text = "[ Blue Shard ]\n" +
                        "A small shard that one day may grow into something bigger.";
                    break;
                case 1:
                    TextBox.text = "[ Silver Stone ]\n" +
                        "A stone that is able to connect to the soul.";

                    TurnOnTop();
                    TurnOnLeft();
                    break;
                case 2:
                    TextBox.text = "[ Emerald Gem ]\n" +
                        "The gem that can summon the poewers of the deity in the atmosphere.";

                    TurnOnTop();
                    TurnOnLeft();
                    break;
                case 3:
                    TextBox.text = "[ Platinum Crystal ]\n" +
                        "This crystalized item can open a portal to a distorted world.";
                    break;
            }
        }
    }

    public void TurnOffWindows()
    {
        foreach(GameObject goWindow in WindowsArray)
        {
            if (goWindow.activeInHierarchy)
            {
                goWindow.SetActive(false);
            }
        }
    }

    public void TurnOnWindow(int intW)
    {
        if (!WindowsArray[intW].activeInHierarchy)
        {
            WindowsArray[intW].SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData PED)
    {
        DragTurning = true;
        PEDPos_O = PED.pressPosition;

        if (intStep == 0)
        {
            TurnOffWindows();
        }
    }

    public void OnDrag(PointerEventData PED)
    {
        PEDPos_F = PED.position;

        fltRot = -PED.delta.x / fltRotThreshold;

        OptionsWheelT[intStep].eulerAngles += ((Vector3.forward) * (fltRot));
    }

    public void OnEndDrag(PointerEventData PED)
    {
        var imgCast = GetComponent<Image>();

        if (imgCast)
        {
            imgCast.raycastTarget = false;
        }

        fltPEDDif = (PEDPos_F - PEDPos_O).normalized.x;

        if (fltPEDDif > 0f)
        {
            if(intPos > 0)
            {
                intPos--;

                PostWheelPos = PreWheelPos - (Vector3.forward * 90f);

                if(intPos == 0)
                {
                    // Turn on Top, Turn off Left
                    TurnOnTop();
                    TurnOffLeft();
                }
            }
            else
            {
                PostWheelPos = Vector3.forward * 360f;
            }
        }
        else if(fltPEDDif < 0f)
        {
            if(intPos < 3)
            {
                intPos++;

                PostWheelPos = PreWheelPos + (Vector3.forward * 90f);

                if (intPos == 3)
                {
                    // Turn off Top, Turn on Left
                    TurnOffTop();
                    TurnOnLeft();
                }
            }
        }

        PreWheelPos = OptionsWheelT[intStep].eulerAngles;

        PEDPos_O = Vector2.zero;
        PEDPos_F = Vector2.zero;

        tTurnTime = 0f;

        DragTurning = false;

        var slider = FindObjectOfType<SliderValue>();

        if (slider)
        {
            slider.ResetValue();
        }
    }

    void TurnOnTop()
    {
        switch (intStep)
        {
            case 0:
                TopMask.color = Color.clear;
                break;
            case 1:
                TopSMask.color = Color.clear;
                break;
        }
    }

    void TurnOnLeft()
    {
        switch (intStep)
        {
            case 0:
                LeftMask.color = Color.clear;
                break;
            case 1:
                LeftSMask.color = Color.clear;
                break;
        }
    }

    void TurnOffTop()
    {
        switch (intStep)
        {
            case 0:
                TopMask.color = Color.black;
                break;
            case 1:
                TopSMask.color = Color.black;
                break;
        }
    }

    void TurnOffLeft()
    {
        switch (intStep)
        {
            case 0:
                LeftMask.color = Color.black;
                break;
            case 1:
                LeftSMask.color = Color.black;
                break;
        }
    }

    public void OnPointerDown(PointerEventData PED)
    {
        PEDPos_O = PED.pressPosition;

        var img = OptionsWheelT[intStep].GetComponent<Image>();

        img.color = ColorPressDown;
    }

    public void OnPointerUp(PointerEventData PED)
    {
        PEDPos_F = PED.position;

        var img = OptionsWheelT[intStep].GetComponent<Image>();

        img.color = ColorPressUp;

        if (PEDPos_O == PEDPos_F)
        {
            ClickProcess();
        }
    }

    public void ClickProcess()
    {
        
        switch (intPos)
        {
            case 0:
                NextStep();
                break;
            case 1:
                // Break only when implemented, display the real Upgrade menu
            case 2:
                // Break only when implemented, load all materials' data and then open it's window
            case 3:
                // When implemented, load all recipes and then open it's window
                if (intStep == 0)
                {
                    TurnOnWindow(intPos);
                }
                break;
        }
    }

    public void NextStep()
    {
        if (intStep < 1)
        {
            intStep++;

            intPos = 0;

            PreWheelPos = OptionsWheelT[intStep].eulerAngles;

            PostWheelPos = PreWheelPos;

            if (MainAnim)
            {
                MainAnim.Play("Step" + intStep.ToString() + "Anim");
            }
        }
    }

    public void PrevStep()
    {
        if (intStep > 0)
        {
            var slider = FindObjectOfType<SliderValue>();

            if (slider)
            {
                slider.ResetValue();
            }

            TurnOffWindows();

            intStep--;

            intPos = 0;

            OptionsWheelT[1].eulerAngles = Vector3.zero;

            PreWheelPos = OptionsWheelT[intStep].eulerAngles;

            PostWheelPos = PreWheelPos;

            if (MainAnim)
            {
                MainAnim.Play("Step" + intStep.ToString());
            }
        }
    }

    public void TurnLeft()
    {
        TurnOffWindows();

        TurnOffRaycasts();

        if (intPos > 0)
        {
            intPos--;

            PostWheelPos = PreWheelPos - (Vector3.forward * 90f);

            if (intPos == 0)
            {
                // Turn on Top, Turn off Left
                TurnOnTop();
                TurnOffLeft();
            }
        }

        tTurnTime = 0f;

        DragTurning = false;
    }

    public void TurnRight()
    {
        TurnOffWindows();

        TurnOffRaycasts();

        if (intPos < 3)
        {
            intPos++;

            PostWheelPos = PreWheelPos + (Vector3.forward * 90f);

            if (intPos == 3)
            {
                // Turn off Top, Turn on Left
                TurnOffTop();
                TurnOnLeft();
            }
        }

        tTurnTime = 0f;

        DragTurning = false;
    }

    void TurnOffRaycasts()
    {
        foreach(Image imgRay in imgRaycasts)
        {
            if (imgRay.raycastTarget)
            {
                imgRay.raycastTarget = false;
            }
        }
    }

    void TurnOnRaycasts()
    {
        foreach(Image imgRay in imgRaycasts)
        {
            if (!imgRay.raycastTarget)
            {
                imgRay.raycastTarget = true;
            }
        }
    }

    public void SendSummon()
    {
        var WheelScript = FindObjectOfType<WheelAnimScript>();

        var sliderV = FindObjectOfType<SliderValue>();

        if (WheelScript)
        {
            WheelScript.SetSynthSprite(SynthRenders[intPos].sprite, SynthRenders[intPos].color);
        }

        TurnOffWindows();

        var camera = FindObjectOfType<RouletteCameraBehavior>();

        if (camera)
        {
            camera.SwitchToWheel();
        }

        if (MainAnim)
        {
            MainAnim.Play("FinalStep");
        }

        Invoke("BeginAnimation", 3.5f);
    }

    void BeginAnimation()
    {
        var WheelAnim = FindObjectOfType<WheelAnimScript>();

        if (WheelAnim)
        {
            WheelAnim.BeginAnimation();
        }
    }
}
