using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterMovement {

    protected override void Awake() {
        base.Awake();
    }

    private void OnEnable() {
        GameManager.instance.Player = this.gameObject;
    }

    protected override void Start() {
        base.Start();

        PlayerInput input = new PlayerInput();
        input.Enable();

        input.Player.Direction.performed += Direction;
        input.Player.Direction.canceled += Direction;

        input.Player.Jump.performed += Jump;
        input.Player.Jump.canceled += Jump;

        input.Player.Sprint.performed += Sprint;
        input.Player.Sprint.canceled += Sprint;

        input.Player.Dash.performed += Dash;
    }

    protected override void Update() {
        base.Update();
    }

    private void Direction(InputAction.CallbackContext context) {
        Vector2 input;
        if (context.performed) {
            input = context.ReadValue<Vector2>();
        } else {
            input = Vector2.zero;
        }
        setDirection(input);
    }

    private void Sprint(InputAction.CallbackContext context) {
        if (context.performed) {
            setSprint(true);
            return;
        }
        setSprint(false);
    }

    private void Jump(InputAction.CallbackContext context) {
        if (context.performed) {
            setJump(true);
            return;
        }
        setJump(false);
    }

    private void Dash(InputAction.CallbackContext context) {
        setDash();
    }
}
