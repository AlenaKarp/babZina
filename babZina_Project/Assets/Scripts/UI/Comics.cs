//this empty line for UTF-8 BOM header
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comics : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Button clickButton;
    [SerializeField] private List<GameObject> frames;

    private int currentFrameIndex = 0;

    private void Awake()
    {
        uiManager.OnInited += OnInited;
        OnInited();

        clickButton.onClick.AddListener(NextFrame);
        NextFrame();
    }

    private void OnDestroy()
    {
        uiManager.OnInited -= OnInited;
    }

    private void OnInited()
    {
        if (uiManager.SaveManagerProvider != null
            && uiManager.SaveManagerProvider.GetLastLevelWithProgress().HasValue == true)
        {
            Destroy(gameObject);
        }
    }

    private void NextFrame()
    {
        if (currentFrameIndex == frames.Count)
        {
            clickButton.onClick.RemoveAllListeners();

            Destroy(gameObject);

            return;
        }

        SetNewFrame(currentFrameIndex);
        currentFrameIndex++;
    }

    private void SetNewFrame(int index)
    {
        for (int i = 0; i <= frames.Count - 1; i++)
        {
            frames[i].SetActive(i == index);
        }
    }
}
