using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] Vector3 startPosition = Vector3.zero, endPosition = Vector3.zero;
    [SerializeField] float timer = 0.0f, maxTime = 5.0f;
    [SerializeField] bool canMove = true;
    [SerializeField] Rigidbody rb = null;

    void Start()
    {
        transform.position = startPosition;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!canMove || !rb) return;

        timer += Time.deltaTime;

        if (timer >= maxTime)
        {
            SwitchVector(ref startPosition, ref endPosition);
            timer = 0.0f;
        }

        float _t = EaseInOutCirc(timer / maxTime);
        Vector3 _targetPos = Vector3.Lerp(startPosition, endPosition, _t);
        rb.MovePosition(_targetPos);
    }

    void SwitchVector(ref Vector3 _start, ref Vector3 _end)
    {
        Vector3 _temp = _start;
        _start = _end;
        _end = _temp;
    }

    float EaseInOutCirc(float _t)
    {
        if( _t <= 0.5f )
        {
            return (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * _t, 2))) / 2;
        }
        return (Mathf.Sqrt(1 - Mathf.Pow(-2 * _t + 2, 2)) + 1) / 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPosition, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPosition, 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startPosition, endPosition);
        Gizmos.color = Color.white;
    }
}
