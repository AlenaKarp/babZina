//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

public class CameraManager : MonoBehaviour, ICameraManager
{
    public Transform MainCamerTransform => mainCamera.transform;

    [SerializeField] Camera mainCamera;

    internal IPlayerPhysics PlayerPhysicsProvider => playerPhysics;

    private IPlayerPhysics playerPhysics;

    internal void Init(IPlayerPhysics playerPhysics)
    {
        this.playerPhysics = playerPhysics;
    }

    public Ray ScreenPointToRay(Vector3 value)
    {
        return mainCamera.ScreenPointToRay(value);
    }
}
