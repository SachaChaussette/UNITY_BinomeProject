using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;

    [SerializeField] float moveSpeed = 5f, rotationSpeed = 10f;

    InputAction moveAction = null;
    InputAction rotateAction = null;

    [SerializeField] bool canMove = true;

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
    }

    void MoveManual()
    {
        if (!canMove) return;

        Vector2 _dir = moveAction.ReadValue<Vector2>();
        if (_dir == Vector2.zero) return;

        float _inputMagnitude = Mathf.Clamp01(_dir.magnitude);

        float _easedMagnitude = EaseInCirc(_inputMagnitude);

        Vector3 _move = (transform.forward * _dir.y + transform.right * _dir.x).normalized * _easedMagnitude * moveSpeed;

        owner.Rigidbody.linearVelocity = new Vector3(_move.x, owner.Rigidbody.linearVelocity.y, _move.z);
    }

    void RotateManual()
    {
        Vector2 _axis = rotateAction.ReadValue<Vector2>();
        //transform.eulerAngles += transform.right * _axis.y * rotationSpeed * Time.deltaTime;
        transform.eulerAngles += transform.up * _axis.x * rotationSpeed * Time.deltaTime;
    }

    float EaseInCirc(float _t)
    {
        return 1 - Mathf.Sqrt(1 - Mathf.Pow(_t, 2));
    }

private void OnDrawGizmos()
    {
        
    }
}
