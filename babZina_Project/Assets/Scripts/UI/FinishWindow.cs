//this empty line for UTF-8 BOM header
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishWindow : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject window;

    private void Awake()
    {
        GetComponentInParent<UIManager>().OnWinWindowOpen += OnWinWindowOpen;

        continueButton.onClick.AddListener(OnContinue);
    }

    private void OnWinWindowOpen()
    {
        window.SetActive(true);
    }

    private void OnContinue()
    {
        Destroy(gameObject);
    }
}
