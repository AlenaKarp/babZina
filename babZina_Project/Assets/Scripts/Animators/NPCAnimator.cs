//this empty line for UTF-8 BOM header
using System;
using UnityEngine;
using UnityTools.Runtime.StatefulEvent;

public class NPCAnimator : MonoBehaviour
{
    [SerializeField] private Observer observer;
    [SerializeField] private Animator animator;

    private const string animatorParameter_StateID_Int_Name = "ID";

    private static readonly int animatorParameter_StateID_Int_Id = Animator.StringToHash(animatorParameter_StateID_Int_Name);

    private void Awake()
    {
        observer.StateID.OnValueChanged += OnNewState;
        observer.OnDisappointState += OnDisappointState;
    }

    private void OnDisappointState(int id)
    {
        animator.SetInteger(animatorParameter_StateID_Int_Id, id);
    }

    private void OnNewState(int ID)
    {
        animator.SetInteger(animatorParameter_StateID_Int_Id, ID);
    }
}
