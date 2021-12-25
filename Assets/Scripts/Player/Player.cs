using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement {
    void Awake() {
        GameManager.Instance.player = this.gameObject;
    }

    private bool usingPower = false, ablePower = true;

    protected override void Start() {
        base.Start();
    }


    protected override void Update() {
        getInput();
        if (power && grounded && ablePower) {
            StartCoroutine(UsePower());
        }
        base.Update();
    }

    private void getInput() {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        jump = Input.GetKeyDown(KeyCode.Space);
        power = Input.GetKeyDown(KeyCode.LeftControl);
    }

    private IEnumerator UsePower() {
        playUsingPower();
        ablePower = false;
        usingPower = true;
        ableMove = false;
        yield return new WaitForSeconds(1.0f);
        usingPower = false;
        ableMove = true;
        yield return new WaitForSeconds(1.0f);
        playRecoverPower();
        yield return new WaitForSeconds(4.0f);
        ablePower = true;
    }

    public enum Interactable {
        Ladder,
        Jump,
    }

    public override void EnterInteractable(Interactable interactable) {
        base.EnterInteractable(interactable);
        switch (interactable) {
            case Interactable.Ladder:
                ladder = true;
                break;
        }

    }

    public override void LeaveInteractable(Interactable st) {
        base.LeaveInteractable(st);
        switch (st) {
            case Interactable.Ladder:
                ladder = false;
                break;
        }
    }

    public bool isUsingPower() {
        return usingPower;
    }
}
