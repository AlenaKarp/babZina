//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    internal event Action OnTutorialStepEnd = () => { };

    protected void FinishStep()
    {
        OnTutorialStepEnd();
    }
}
