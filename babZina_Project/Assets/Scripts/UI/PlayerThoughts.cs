//this empty line for UTF-8 BOM header
using UnityEngine;

public class PlayerThoughts : MonoBehaviour
{
    [SerializeField] PlayerPhysics playerPhysics;
    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        Vector3 playerPosition2D = playerPhysics.PlayerPosition;
        playerPosition2D.z = 0;

        transform.position = playerPosition2D + offset;
    }
}
