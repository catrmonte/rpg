using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        // does the player exist?
        if (PlayerController.me != null && !PlayerController.me.dead)
        {
            UnityEngine.Debug.Log("The player still exists in camera");
            Vector3 targetPos = PlayerController.me.transform.position;
            targetPos.z = -10;

            transform.position = targetPos;
        }
    }
}
