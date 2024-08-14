//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Step3 : TutorialStep
{
    [Serializable]
    public struct StateMessage
    {
        public string message;
        public IInteractiveObject.State state;
    }

    [SerializeField] private List<StateMessage> stateMessages;
    [SerializeField] private string gameOverMessage;
    [SerializeField] private TMP_Text message;
    [SerializeField] private InteractiveObject interactiveObject;
    [SerializeField] private GameObject textPopup;

    private void Awake()
    {
        interactiveObject.CurrentState.OnValueChanged += OnInteractiveObjectValueChanged;
        interactiveObject.OnGameOver += OnGameOverMessage;
    }

    private void OnGameOverMessage()
    {
        SetMessage(gameOverMessage);
    }

    private void OnInteractiveObjectValueChanged(IInteractiveObject.State state)
    {
        if (state == IInteractiveObject.State.AfterInteract)
        {
            FinishStep();
            return;
        }

        SetMessage(GetMessageByState(state));
    }

    private void SetMessage(string newMessage)
    {
        textPopup.SetActive(string.IsNullOrEmpty(newMessage) == false);
        this.message.text = newMessage;
    }

    private string GetMessageByState(IInteractiveObject.State state)
    {
        foreach (StateMessage stateMessage in stateMessages)
        {
            if (stateMessage.state == state)
            {
                return stateMessage.message;
            }
        }

        return "";
    }

}
