using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform target;
    private Vector3 offset;

    [SerializeField, Range(1, 10)]
    private float smoothFactor;

    void Start() {
        target = GameManager.Instance.player.transform;
    }

    void FixedUpdate() {
        follow();
    }

    private void follow() {
        Vector3 targetPos = target.position + (Vector3.back * 10);

        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.fixedDeltaTime);

        transform.position = smoothPos;
    }

}
