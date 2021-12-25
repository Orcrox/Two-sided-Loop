using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {
    private Animator animator;

    [SerializeField] private ParticleSystem PSRecoverPower, PSUsingPower;

    //input
    protected Vector2 input;
    protected float VSpeed;
    protected bool jump;
    protected bool power;
    protected bool grounded;

    //states
    protected bool ladder;
    protected bool ableMove;

    protected virtual void Start() {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update() {
        if (!ableMove) {
            animator.SetFloat("HSpeed", 0);
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("VSpeed", 0);
            return;
        }

        if (input.x != 0) {
            animator.SetFloat("InputH", input.x);
        }

        animator.SetFloat("HSpeed", Mathf.Abs(input.x));
        animator.SetFloat("VSpeed", VSpeed);
        animator.SetBool("Grounded", grounded);
    }

    protected void playUsingPower() {
        PSUsingPower.Play();
    }
    protected void playRecoverPower() {
        PSRecoverPower.Play();
    }

    public virtual void EnterInteractable(Player.Interactable interactable) { }

    public virtual void LeaveInteractable(Player.Interactable interactable) { }

}
