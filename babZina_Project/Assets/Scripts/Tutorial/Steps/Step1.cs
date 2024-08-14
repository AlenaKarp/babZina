//this empty line for UTF-8 BOM header
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step1 : TutorialStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishStep();
        }
    }
}
