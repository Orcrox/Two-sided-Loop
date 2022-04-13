using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterAnimation {
    private Rigidbody2D rb;

    private Vector2 direction;

    [SerializeField] private bool sprint;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;

    [SerializeField] private bool jumping;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 5f;

    [SerializeField] private bool onGround = true;
    [SerializeField] private bool onWall;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float sideOffset, downOffset;
    [SerializeField] private float castSize;

    [SerializeField] private bool dashing;

    protected override void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start() {
        base.Start();
        direction = Vector2.zero;

        movementSpeed = walkSpeed;
    }

    protected override void Update() {
        base.Update();
        updateAnimationMovement(direction, rb.velocity);
    }

    protected void setDirection(Vector2 input) {
        direction = input;
    }

    protected void setSprint(bool state) {
        sprint = state;
        setSprintAnim(sprint);
        if (sprint) {
            movementSpeed = sprintSpeed;
            return;
        }
        movementSpeed = walkSpeed;
    }

    protected void setJump(bool state) {
        if (!onGround) {
            if (!state) {
                jumping = state;
            }
            return;
        }
        jumping = state;
        if (jumping) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            setJumpAnim();
        }
    }

    protected void setDash() {
        if (!dashing) {
            dashing = true;
            setDashAnim();
            startDash();
        }
    }

    protected void startDash() {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.AddForce(direction * 50, ForceMode2D.Impulse);
        rb.drag = 8;
    }

    public void endDash() {
        rb.gravityScale = 1;
        rb.drag = 0;
        dashing = false;
        endDashAnim();
    }

    private void FixedUpdate() {
        if (!dashing) {
            move();
            betterJump();
        }
        getRaycast();
        if (onWall && !onGround && !jumping && !dashing) {
            wallSlide();
        }
    }

    private void move() {
        rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
        if (direction.x != 0) {
            GetComponent<Transform>().localScale = new Vector3(direction.x, 1, 1);
        }
    }

    private void betterJump() {
        if (rb.velocity.y < 0) {
            jumping = false;
            rb.gravityScale = fallMultiplier;
        } else if (0 < rb.velocity.y && !jumping) {
            rb.gravityScale = lowJumpMultiplier;
        } else {
            rb.gravityScale = 1f;
        }
    }

    private void wallSlide() {
        rb.velocity = new Vector2(rb.velocity.x, -0.5f);
    }

    private void getRaycast() {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.down * downOffset, castSize, layer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + Vector2.left * sideOffset, castSize, layer)
            || Physics2D.OverlapCircle((Vector2)transform.position + Vector2.right * sideOffset, castSize, layer);
        setOn(onGround, onWall);
        //onGround = Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 0.5f, layer);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * downOffset, castSize);
        Gizmos.DrawWireSphere(transform.position + Vector3.left * sideOffset, castSize);
        Gizmos.DrawWireSphere(transform.position + Vector3.right * sideOffset, castSize);
        //Gizmos.DrawWireCube(transform.position + Vector3.down * 0.5f, transform.localScale);
    }
}
