using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] bool canMove = true;

    [SerializeField] float groundDist = 1.1f;
    [SerializeField] LayerMask platformLayer = 0;

    [SerializeField] float stickForce = 10f;

    InputAction moveAction = null;
    InputAction rotateAction = null;

    Rigidbody currentPlatformRb = null;
    Vector3 lastPlatformPos;
    Quaternion lastPlatformRot;
    bool onPlatform = false;

    public void SetCanMove(bool _canMove)
    {
        if (canMove == _canMove) return;
        canMove = _canMove;
    }

    public void Init(InputAction _moveAction, InputAction _rotateAction)
    {
        owner = GetComponent<Player>();
        moveAction = _moveAction;
        rotateAction = _rotateAction;

        owner.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

    }

    void FixedUpdate()
    {
        CheckPlatform();
        MoveManual();
        RotateManual();
        ApplyPlatformMovement();
    }

    void MoveManual()
    {
        if (!canMove) return;

        Vector2 _dir = moveAction.ReadValue<Vector2>();

        if (_dir.sqrMagnitude > 0.01f)
        {
            float _inputMagnitude = Mathf.Clamp01(_dir.magnitude);
            float _easedMagnitude = EaseInCirc(_inputMagnitude);
            Vector3 _move = (transform.forward * _dir.y + transform.right * _dir.x).normalized * _easedMagnitude * moveSpeed;
            owner.Rigidbody.linearVelocity = new Vector3(_move.x, owner.Rigidbody.linearVelocity.y, _move.z);
        }
        else
        {
            Vector3 _vel = owner.Rigidbody.linearVelocity;
            Vector3 _horizontal = new Vector3(_vel.x, 0, _vel.z);
            _horizontal = Vector3.Lerp(_horizontal, Vector3.zero, Time.fixedDeltaTime * 5f);
            owner.Rigidbody.linearVelocity = new Vector3(_horizontal.x, _vel.y, _horizontal.z);
        }
    }

    void RotateManual()
    {
        Vector2 _axis = rotateAction.ReadValue<Vector2>();
        transform.eulerAngles += transform.up * _axis.x * rotationSpeed * Time.deltaTime;
    }

    float EaseInCirc(float _t)
    {
        return 1 - Mathf.Sqrt(1 - Mathf.Pow(_t, 2));
    }

    void CheckPlatform()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit _hit, groundDist, platformLayer))
        {
            Rigidbody _hitRb = _hit.rigidbody;

            if (_hitRb != null)
            {
                if (!onPlatform || _hitRb != currentPlatformRb)
                {
                    // Nouvelle plateforme
                    currentPlatformRb = _hitRb;
                    lastPlatformPos = _hitRb.position;
                    lastPlatformRot = _hitRb.rotation;
                    onPlatform = true;
                }
            }
        }
        else
        {
            onPlatform = false;
            currentPlatformRb = null;
        }
    }


    void ApplyPlatformMovement()
    {
        if (!onPlatform || currentPlatformRb == null)
            return;

        Vector3 _platformDelta = currentPlatformRb.position - lastPlatformPos;
        Quaternion _platformRotDelta = currentPlatformRb.rotation * Quaternion.Inverse(lastPlatformRot);

        owner.Rigidbody.MovePosition(owner.Rigidbody.position + _platformDelta);
        owner.Rigidbody.MoveRotation(_platformRotDelta * owner.Rigidbody.rotation);

        lastPlatformPos = currentPlatformRb.position;
        lastPlatformRot = currentPlatformRb.rotation;
    }
}
