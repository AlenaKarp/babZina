//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityEngine.UI;

public class URLButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string URL;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Application.OpenURL(URL);
    }
}
