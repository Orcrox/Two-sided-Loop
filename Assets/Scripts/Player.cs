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
        getInput();
        move();
        jump();
    }

    private void getInput() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void move() {
        rb.velocity = new Vector2(input.x, rb.velocity.y);
    }

    private void jump() {
        if (input.y == 1) {
            rb.velocity = new Vector2(rb.velocity.x, input.y * 5);
        }
    }

}
