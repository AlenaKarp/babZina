//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] private NewGame newGameWindow;
    [SerializeField] private Vector3 mainMenuOffset;
    [SerializeField] private Vector3 newGameOffset;
    [SerializeField] private float speed;

    private bool isMainMenuState = true;

    private void Awake()
    {
        newGameWindow.OnNewGameMenu += ChangeCameraTarget;
    }

    private void ChangeCameraTarget()
    {
        isMainMenuState = !isMainMenuState;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = isMainMenuState ? mainMenuOffset : newGameOffset;

        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed);
    }
}
