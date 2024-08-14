//this empty line for UTF-8 BOM header
using UnityEngine;

public interface ICameraManager
{
    Transform MainCamerTransform { get; }
    Ray ScreenPointToRay(Vector3 value);
}
