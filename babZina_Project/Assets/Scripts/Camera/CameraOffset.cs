//this empty line for UTF-8 BOM header
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private CameraManager cameraManager;

    private void LateUpdate()
    {
        if (cameraManager.PlayerPhysicsProvider == null)
        {
            return;
        }

        transform.position = cameraManager.PlayerPhysicsProvider.PlayerPosition + offset;
    }
}
