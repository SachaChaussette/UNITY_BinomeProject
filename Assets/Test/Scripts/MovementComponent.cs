using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class MovementComponent : MonoBehaviour
{
    InputAction moveAction = null;
    InputAction rotateAction = null;
    InputAction jumpAction = null;

	[SerializeField] float moveSpeed = 5.0f, rotateSpeed = 50.0f, jumpForce = 10.0f;
    [SerializeField] bool isGrounded = false;
	[SerializeField] Rigidbody rb = null;
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] CinemachineRotationComposer composer = null;

    void Start()
	{
        rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate()
	{
        Move();
        Rotate();
        CheckIsGrounded();
    }

    private void LateUpdate()
    {
        RotateUpCamera();
    }

    public void Init(InputAction _moveAction, InputAction _rotateAction, InputAction _jumpAction)
	{
		moveAction = _moveAction;
		rotateAction = _rotateAction;
		jumpAction = _jumpAction;

		jumpAction.performed += Jump;
    }

	void Move()
	{
		if (moveAction == null) return;

		Vector2 _dir = moveAction.ReadValue<Vector2>();

        //transform.position += (transform.forward * _dir.y + transform.right * _dir.x) * moveSpeed * Time.deltaTime;

        Vector3 _move = (transform.forward * _dir.y + transform.right * _dir.x) * moveSpeed;
        Vector3 _velocity = new Vector3(_move.x, rb.linearVelocity.y, _move.z);

        rb.linearVelocity = _velocity;
        //rb.Move(transform.position + _velocity * Time.deltaTime, rb.rotation);
    }

    void Rotate()
    {
        if (rotateAction == null) return;

        Vector2 _dir = rotateAction.ReadValue<Vector2>();
        if (_dir.sqrMagnitude < 0.001f) return;

        float yaw = _dir.x * rotateSpeed * Time.deltaTime;
        float pitch = -_dir.y * rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, yaw, Space.World);
        //transform.Rotate(Vector3.right, pitch, Space.Self);

        //Quaternion _rot = Quaternion.Euler(transform.up * _dir.x * rotateSpeed * Time.deltaTime) * rb.rotation;
        //rb.MoveRotation(_rot);
    }

    void RotateUpCamera()
    {
        //Vector2 _dir = rotateAction.ReadValue<Vector2>();
        //composer.TargetOffset += new Vector3(0, _dir.y * Time.deltaTime, 0);
    }

    void Jump(InputAction.CallbackContext _context)
	{
        if (!rb) return;

        if (isGrounded && rb.linearVelocity.y < 0.1f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // reset Y
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    void CheckIsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}