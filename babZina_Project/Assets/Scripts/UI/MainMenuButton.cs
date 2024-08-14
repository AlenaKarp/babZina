//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void OnMainMenu()
    {
        SceneManager.LoadScene(SceneNames.mainMenu);
    }
}
