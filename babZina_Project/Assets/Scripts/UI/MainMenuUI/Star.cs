//this empty line for UTF-8 BOM header
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject full;

    internal void SetVisible(bool hasValue)
    {
        full.SetActive(hasValue);
    }
}
