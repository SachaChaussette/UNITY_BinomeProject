using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] InputComponent inputs = null;
    [SerializeField] MovementComponent movement = null;
    [SerializeField] DashComponent dash = null;
    [SerializeField] JumpComponent jump = null;

    [SerializeField] Rigidbody rb = null;

    public InputComponent Inputs => inputs;
    public MovementComponent Movement => movement;
    public DashComponent Dash => dash;
    public JumpComponent Jump => jump;
    public Rigidbody Rigidbody => rb;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {
        inputs = GetComponent<InputComponent>();
        movement = GetComponent<MovementComponent>();
        dash = GetComponent<DashComponent>();
        jump = GetComponent<JumpComponent>();
        rb = GetComponent<Rigidbody>();
        
        if (!inputs || !movement) return;
        movement.Init(inputs.MoveAction, inputs.RotateAction);
        if (!dash) return;
        dash.Init(inputs.DashAction);
        if (!jump) return;
        jump.Init(inputs.JumpAction);

        Cursor.lockState = CursorLockMode.Locked;
    }
}
