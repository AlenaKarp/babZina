//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Step3 : TutorialStep
{
    [Serializable]
    public struct StateMessage
    {
        public string messageLocalizationKey;
        public IInteractiveObject.State state;
    }

    [SerializeField] private List<StateMessage> stateMessages;
    [SerializeField] private string gameOverMessage;
    [SerializeField] private TMP_Text message;
    [SerializeField] private InteractiveObject interactiveObject;
    [SerializeField] private GameObject textPopup;

    private Dictionary<IInteractiveObject.State, string> stateLocalization;

    private void Awake()
    {
        stateLocalization = new Dictionary<IInteractiveObject.State, string>();
        foreach(StateMessage stateMessage in stateMessages)
        {
            LocalizationSettings.StringDatabase.GetLocalizedStringAsync(stateMessage.messageLocalizationKey).Completed += 
            asyncResult => CacheLocalization(stateMessage.state, asyncResult.Result); 
        }

        interactiveObject.CurrentState.OnValueChanged += OnInteractiveObjectValueChanged;
        interactiveObject.OnGameOver += OnGameOverMessage;
    }

    private void CacheLocalization(IInteractiveObject.State state, string message)
    {
        stateLocalization.Add(state, message);
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
        return stateLocalization[state];
    }
}
