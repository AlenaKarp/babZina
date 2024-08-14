//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private Animator animator;

    private const string animatorParameter_StateIndex_Int_Name = "State";

    private static readonly int animatorParameter_StateIndex_Int_Id = Animator.StringToHash(animatorParameter_StateIndex_Int_Name);

    private void Awake()
    {
        playerView.OnNewState += OnNewState;
    }

    private void OnNewState(PlayerPhysics.State state)
    {
        animator.SetInteger(animatorParameter_StateIndex_Int_Id, (int)state);
    }
}
