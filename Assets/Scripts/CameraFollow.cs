using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    void Start()
    {
        target = GameManager.Instance.player.transform;
    }

    void FixedUpdate()
    {
    }

    private void follow()
    {
        transform.position = target.position + (Vector3.back * 10);
    }

}
