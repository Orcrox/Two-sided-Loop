using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 input;
    private Animator anim;

    private float speed;

    void Awake() {
        GameManager.Instance.player = this.gameObject;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        speed = 5.0f;
    }


    private void Update() {
        getInput();
        move();
        jump();
    }

    private void getInput() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
    }

    private void move() {
        if (input.x != 0) {
            transform.localScale = new Vector3(input.x, 1, 1);
        }
        anim.SetFloat("Speed", input.sqrMagnitude);
        anim.SetFloat("VSpeed", rb.velocity.y);
        anim.SetBool("Grounded", isGrounded());
        rb.velocity = new Vector2(input.x * speed, rb.velocity.y);
    }

    private void jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            anim.SetBool("Grounded", isGrounded());
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

    }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

}
