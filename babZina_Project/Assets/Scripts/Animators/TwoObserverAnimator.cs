//this empty line for UTF-8 BOM header
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Runtime.StatefulEvent;

public class TwoObserverAnimator : MonoBehaviour
{
    [SerializeField] private List<Observer> observers;
    [SerializeField] private List<Animator> animators;

    private const string animatorParameter_StateID_Int_Name = "ID";
    private const string animatorParameter_DisappointID_Int_Name = "DisappointID";
    private const string animatorParameter_TotalDisappoint_Int_Name = "TotalDisappoint";

    private static readonly int animatorParameter_StateID_Int_Id = Animator.StringToHash(animatorParameter_StateID_Int_Name);
    private static readonly int animatorParameter_DisappointID_Int_Id = Animator.StringToHash(animatorParameter_DisappointID_Int_Name);
    private static readonly int animatorParameter_TotalDisappoint_Int_Id = Animator.StringToHash(animatorParameter_TotalDisappoint_Int_Name);

    bool hasDissapointState = false;

    private void Awake()
    {
        foreach (Observer observer in observers)
        {
            observer.OnDisappointState += OnDisappointState;
            observer.StateID.OnValueChanged += OnStateValueChanged;
        }
    }

    private void OnDestroy()
    {
        foreach (Observer observer in observers)
        {
            observer.OnDisappointState -= OnDisappointState;
            observer.StateID.OnValueChanged -= OnStateValueChanged;
        }
    }

    private void OnStateValueChanged(int id)
    {
        foreach (Animator animator in animators)
        {
            animator.SetInteger(animatorParameter_StateID_Int_Id, id);
        }
    }

    private void OnDisappointState(int id)
    {
        if (hasDissapointState == true)
        {
            foreach (Animator animator in animators)
            {
                animator.SetBool(animatorParameter_TotalDisappoint_Int_Id, true);
            }
        }

        foreach (Animator animator in animators)
        {
            animator.SetInteger(animatorParameter_DisappointID_Int_Id, id);
        }

        hasDissapointState = true;
    }
}
