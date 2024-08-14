//this empty line for UTF-8 BOM header
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        audioMixer.SetFloat("Volume", ConvertToDb(value));
    }

    private float ConvertToDb(float value)
    {
        if (value < 0.001f)
        {
            return -80;
        }

        return 20f * Mathf.Log10(value);
    }
}
