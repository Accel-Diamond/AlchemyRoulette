using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MasterAnimScripts : MonoBehaviour
{
    
    public void TextBoxNeededUpdate()
    {
        var wheel = FindObjectOfType<OptionWheelBehavior>();

        if (wheel)
        {
            wheel.UpdateTextBox();
        }
    }

    public void HelpOpenWindow()
    {
        var wheel = FindObjectOfType<OptionWheelBehavior>();

        if (wheel)
        {
            wheel.TurnOnWindow(0);
        }
    }

    public void EndGame()
    {
        var anim = GetComponent<Animator>();

        if (anim)
        {
            anim.Play("End");
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}
