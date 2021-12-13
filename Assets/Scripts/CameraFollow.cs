using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = GameManager.Instance.player.transform;
    }


    void Update()
    {
        transform.position = target.position + (Vector3.back * 10);
    }
}
