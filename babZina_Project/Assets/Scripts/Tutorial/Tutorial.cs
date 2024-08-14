//this empty line for UTF-8 BOM header
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<TutorialStep> steps;
    [SerializeField] private Scenario scenario;

    int currentStep = 0;

    private void Awake()
    {
        StartStep(currentStep);
    }

    private void StartStep(int index)
    {
        TurnOffAllSteps();

        steps[index].gameObject.SetActive(true);
        steps[index].OnTutorialStepEnd += OnStepEnd;
    }

    private void OnStepEnd()
    {
        if (currentStep == steps.Count - 1)
        {
            TurnOffAllSteps();
            scenario.PlayWinTutorialLevel();
            return;
        }

        currentStep++;
        StartStep(currentStep);
    }

    private void TurnOffAllSteps()
    {
        foreach (TutorialStep step in steps)
        {
            step.OnTutorialStepEnd -= OnStepEnd;
            step.gameObject.SetActive(false);
        }
    }
}
