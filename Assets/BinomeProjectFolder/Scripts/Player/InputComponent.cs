using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    IAA_Inputs controls = null;
    InputAction moveAction = null;
    InputAction rotateAction = null;
    InputAction dashAction = null;
    public InputAction MoveAction => moveAction;
    public InputAction RotateAction => rotateAction;
    public InputAction DashAction => dashAction;



    private void Awake()
    {
        controls = new IAA_Inputs();
    }

    private void OnEnable()
    {
        moveAction = controls.Player.Move;
        rotateAction = controls.Player.Rotate;
        dashAction = controls.Player.Dash;

        moveAction.Enable();
        rotateAction.Enable();
        dashAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
        dashAction.Disable();
    }
}
