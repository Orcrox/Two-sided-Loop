using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 input;

    void Awake() {
        GameManager.Instance.player = this.gameObject;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update() {
        getMovement();
        move();
    }

    private void getMovement() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void move() {
        rb.velocity = input;
    }

}
