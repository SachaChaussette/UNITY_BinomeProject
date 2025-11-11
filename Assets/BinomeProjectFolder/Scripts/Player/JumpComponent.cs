using UnityEngine;
using UnityEngine.InputSystem;

public class JumpComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;

    [SerializeField] float jumpForce = 7f;
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] LayerMask groundMask;

    InputAction jumpAction = null;

    bool isGrounded;

    void Awake()
    {

    }

    void Update()
    {
        CheckGround();
    }

    public void Init(InputAction _jumpAction)
    {
        owner = GetComponent<Player>();
        jumpAction = _jumpAction;
        jumpAction.performed += Jump;
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundMask);
    }

    void Jump(InputAction.CallbackContext _context)
    {
        Debug.Log("Jump");
        if (!isGrounded) return;
        owner.Rigidbody.linearVelocity = new Vector3(owner.Rigidbody.linearVelocity.x, 0, owner.Rigidbody.linearVelocity.z);
        owner.Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (groundCheckDistance + 0.1f));
    }
}
