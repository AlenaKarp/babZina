//this empty line for UTF-8 BOM header
using System.Collections.Generic;
using UnityEngine;

public class StartDayButtons : MonoBehaviour
{
    [SerializeField] private List<GameObject> stars;
    [SerializeField] private int dayNumber;

    ISaveManager saveManager;

    private void Awake()
    {
        saveManager = GetComponentInParent<NewGame>().SaveManagerProvider;
    }

    private void OnEnable()
    {
        for (int i = 1; i <= stars.Count; i++)
        {
            stars[i - 1].SetActive(i <= saveManager.GetProgress(dayNumber));
        }
    }
}
