//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLevelButton : MonoBehaviour
{
    [SerializeField] private string loadingSceneName;

    public void OnClick()
    {
        SceneManager.LoadScene(loadingSceneName);
    }
}
