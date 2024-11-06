using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private SceneLoader sceneLoader;

    public void SetNewSceneName()
    {
        sceneLoader.SetNewSceneName(sceneName);
    }
}
