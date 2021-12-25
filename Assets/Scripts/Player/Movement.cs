using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Anim {
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed;

    protected override void Start() {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        speed = 5.0f;
        ableMove = true;
    }

    private void FixedUpdate() {
        if (!ableMove) {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        float x, y;
        grounded = isGrounded();
        if (ladder) {
            x = input.x * speed;
            y = input.y * speed;
        } else if (jump && grounded) {
            x = input.x * speed;
            y = speed;
        } else {
            x = input.x * speed;
            y = rb.velocity.y;
        }
        VSpeed = y;
        rb.velocity = new Vector2(x, y);
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public override void EnterInteractable(Player.Interactable st) {
        base.EnterInteractable(st);
        switch (st) {
            case Player.Interactable.Ladder:
                rb.gravityScale = 0;
                break;
            case Player.Interactable.Jump:
                rb.velocity = new Vector2(rb.velocity.x, speed * 2);
                break;
        }
    }

    public override void LeaveInteractable(Player.Interactable interactable) {
        base.LeaveInteractable(interactable);
        switch (interactable) {
            case Player.Interactable.Ladder:
                rb.gravityScale = 1;
                break;
        }
    }
}
