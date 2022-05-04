using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimScript : MonoBehaviour
{
    public GameObject SynthWindow;

    private byte synthValue;

    public SpriteRenderer finalRender; 

    public ParticleSystem GatheringParticles;

    public ParticleSystem[] MaterialSummon;

    public void TurnOnMaterialSummonParticle(int intMat)
    {
        if (MaterialSummon[intMat])
        {
            MaterialSummon[intMat].Play();
        }
    }

    public void TurnOnGatheringParticle()
    {
        if (GatheringParticles)
        {
            GatheringParticles.Play(true);
        }
    }

    public void SetSynthSprite(Sprite sprSynth, Color clrSynth)
    {
        if (finalRender)
        {
            finalRender.sprite = sprSynth;

            finalRender.color = clrSynth;
        }
    }

    public void SetSynthValue (byte synthValue)
    {
        this.synthValue = synthValue;
    }

    public void OpenSynthWindow()
    {
        if (SynthWindow)
        {
            SynthWindow.SetActive(true);
        }
    }

    public void BeginAnimation()
    {
        var anim = GetComponent<Animator>();

        if (anim)
        {
            anim.enabled = true;
        }
    }
}
