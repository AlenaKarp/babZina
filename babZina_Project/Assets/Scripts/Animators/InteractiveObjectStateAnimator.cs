//this empty line for UTF-8 BOM header
using UnityEngine;

public class InteractiveObjectStateAnimator : MonoBehaviour
{
    [SerializeField] InteractiveObject interactioveObject;
    [SerializeField] private Animator animator;

    private const string animatorParameter_StateID_Int_Name = "ID";

    private static readonly int animatorParameter_StateID_Int_Id = Animator.StringToHash(animatorParameter_StateID_Int_Name);

    private void Awake()
    {
        interactioveObject.CurrentState.OnValueChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        interactioveObject.CurrentState.OnValueChanged -= OnStateChanged;
    }

    private void OnStateChanged(IInteractiveObject.State state)
    {
        animator.SetInteger(animatorParameter_StateID_Int_Id, (int)state);
    }
}
