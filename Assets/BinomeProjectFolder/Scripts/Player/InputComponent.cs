using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    IAA_Inputs controls = null;
    InputAction moveAction = null;
    InputAction rotateAction = null;
    InputAction dashAction = null;
    InputAction jumpAction = null;
    public InputAction MoveAction => moveAction;
    public InputAction RotateAction => rotateAction;
    public InputAction DashAction => dashAction;
    public InputAction JumpAction => jumpAction;



    private void Awake()
    {
        controls = new IAA_Inputs();
    }

    private void OnEnable()
    {
        moveAction = controls.Player.Move;
        rotateAction = controls.Player.Rotate;
        dashAction = controls.Player.Dash;
        jumpAction = controls.Player.Jump;

        moveAction.Enable();
        rotateAction.Enable();
        dashAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
        dashAction.Disable();
        jumpAction.Disable();
    }
}
