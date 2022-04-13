using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {
    private Animator animator;
    private ParticleSystem DashPS;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        DashPS = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    protected virtual void Start() {
        DashPS.Pause();
    }

    protected virtual void Update() { }

    protected void updateAnimationMovement(Vector2 direction, Vector2 velocity) {
        if (direction != Vector2.zero) {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        animator.SetFloat("HSpeed", Mathf.Abs(direction.x));
        animator.SetFloat("VSpeed", velocity.y);
    }

    protected void setSprintAnim(bool sprint) {
        if (sprint) {
            animator.SetFloat("Sprint", 1);
            return;
        }
        animator.SetFloat("Sprint", 0);
    }

    protected void setJumpAnim() {
        animator.SetTrigger("Jump");
    }

    protected void setOn(bool onGround, bool onWall) {
        animator.SetBool("onGround", onGround);
        animator.SetBool("onWall", onWall);
    }

    protected void setDashAnim() {
        animator.SetTrigger("Dash");
        DashPS.Play();
    }

    protected void endDashAnim() {
        DashPS.Stop();
    }
}
