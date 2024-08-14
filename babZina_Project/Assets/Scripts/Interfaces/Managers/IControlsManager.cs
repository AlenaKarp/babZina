//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

public interface IControlsManager
{
    event Action<Vector3> OnNewClickPosition;
    event Action<Collider, Vector3> OnInteractiveObjectClick;
}
