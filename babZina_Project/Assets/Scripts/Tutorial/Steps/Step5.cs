//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Step5 : TutorialStep
{
    [SerializeField] private Button button;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        audioMixer.SetFloat("SoundVolume", -80);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        audioMixer.SetFloat("SoundVolume", 0);
    }

    private void OnClick()
    {
        FinishStep();
    }
}
