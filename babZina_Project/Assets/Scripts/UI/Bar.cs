//this empty line for UTF-8 BOM header
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image scale;

    internal void SetProgress(float value)
    {
        scale.fillAmount = value;
    }
}
