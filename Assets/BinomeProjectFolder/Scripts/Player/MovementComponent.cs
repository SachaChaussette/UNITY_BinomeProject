using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;

    [SerializeField] float moveSpeed = 5f, rotationSpeed = 10f;

    InputAction moveAction = null;
    InputAction rotateAction = null;

    [SerializeField] bool canMove = true;

    [SerializeField] Rigidbody rb = null;

    public void SetCanMove(bool _canMove)
    {
        if (canMove == _canMove) return;
        canMove = _canMove;
    }

    void Update()
    {
        MoveManual();
        RotateManual();
    }

    public void Init(InputAction _moveAction, InputAction _rotateAction)
    {
        owner = GetComponent<Player>();
        moveAction = _moveAction;
        rotateAction = _rotateAction;
        rb = GetComponent<Rigidbody>();
    }

    void MoveManual()
    {
        if (!canMove) return;
        Vector2 _dir = moveAction.ReadValue<Vector2>();
        //transform.position += (transform.forward * _dir.y + transform.right * _dir.x) * moveSpeed * Time.deltaTime;

        if (!rb) return;
        Vector3 _move = (transform.forward * _dir.y + transform.right * _dir.x) * moveSpeed;
        Vector3 _velocity = new Vector3(_move.x, rb.linearVelocity.y, _move.z);

        rb.linearVelocity = _velocity;
    }

    void RotateManual()
    {
        Vector2 _axis = rotateAction.ReadValue<Vector2>();
        //transform.eulerAngles += transform.right * _axis.y * rotationSpeed * Time.deltaTime;
        transform.eulerAngles += transform.up * _axis.x * rotationSpeed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        
    }
}
