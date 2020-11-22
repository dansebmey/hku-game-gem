using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] public Player player;

    private void Update()
    {
        Vector3 camPos = transform.position;
        transform.position = new Vector3(player.transform.position.x, camPos.y, camPos.z);
    }
}
