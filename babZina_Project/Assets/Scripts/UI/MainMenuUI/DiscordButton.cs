//this empty line for UTF-8 BOM header
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscordButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Application.OpenURL("https://discord.gg/4AnSY8CT");
    }
}
