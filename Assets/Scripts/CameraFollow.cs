using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform target;

    private void Start() {
        target = GameManager.instance.Player.GetComponent<Transform>();
    }

    private void Update() {
        transform.position = target.position + Vector3.back * 10;
    }
}
