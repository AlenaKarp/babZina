//this empty line for UTF-8 BOM header
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    internal void ChangeForward(Vector3 newForward)
    {
        transform.rotation = Quaternion.LookRotation(newForward, transform.up);
    }
}
