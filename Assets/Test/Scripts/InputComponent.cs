using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
	IAA_Player controls = null;

	InputAction moveAction = null;
	InputAction rotateAction = null;
	InputAction jumpAction = null;

	public InputAction MoveAction => moveAction;
	public InputAction RotateAction => rotateAction;
	public InputAction JumpAction => jumpAction;


    private void Awake()
    {
        controls = new IAA_Player();
    }

    private void OnEnable()
    {
        moveAction = controls.Player.Move;
        moveAction.Enable();

        rotateAction = controls.Player.Rotate;
        rotateAction.Enable();

        jumpAction = controls.Player.Jump;
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
        jumpAction.Disable();
    }
}