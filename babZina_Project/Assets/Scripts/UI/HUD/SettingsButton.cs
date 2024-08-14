//this empty line for UTF-8 BOM header
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    public void OnClick()
    {
        uiManager.ShowPauseMenu();
    }
}
