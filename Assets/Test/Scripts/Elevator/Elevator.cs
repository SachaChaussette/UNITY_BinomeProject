using Unity.VisualScripting;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] bool canMove = true;
    [SerializeField] float currentTime = 0.0f, maxTime = 3.0f;
    [SerializeField] float distanceOffset = 0.5f;
    [SerializeField] Vector3 basePos = Vector3.zero;
    [SerializeField] ElevatorDoor upperDoor = null, lowerDoor = null;
    

    void Start()
    {
        basePos = transform.position;

        Invoke(nameof(Init), 2.0f);
    }

    void Init()
    {
        canMove = true;
    }

    void Update()
    {
        IncreaseTime();
        Move();
    }

    void Move()
    {
        if (!canMove) return;
        transform.position = Vector3.Lerp(basePos + Vector3.up * distanceOffset, basePos, EaseInOutBounce(currentTime / maxTime));
    }

    void IncreaseTime()
    {
        if(!canMove) return;
        currentTime += Time.deltaTime;

        if(currentTime > maxTime)
        {
            currentTime = 0.0f;
            canMove = false;

            if (!upperDoor || !lowerDoor) return;
            Invoke(nameof(OpenDoors), 0.5f);
        }
    }

    void OpenDoors()
    {
        upperDoor.CanMove = true;
        lowerDoor.CanMove = true;
    }

    float EaseInOutBounce(float _t)
    {
        return _t < 0.5
        ? (1 - EaseOutBounce(1 - 2 * _t)) / 2
        : (1 + EaseOutBounce(2 * _t - 1)) / 2;
    }

    float EaseOutBounce(float _t)
    {
        const float _n1 = 7.5625f;
        const float _d1 = 2.75f;

        if (_t < 1.0f / _d1)
        {
            return _n1 * _t * _t;
        }
        else if (_t < 2 / _d1)
        {
            return _n1 * (_t -= 1.5f / _d1) * _t + 0.75f;
        }
        else if (_t < 2.5 / _d1)
        {
            return _n1 * (_t -= 2.25f / _d1) * _t + 0.9375f;
        }
        else
        {
            return _n1 * (_t -= 2.625f / _d1) * _t + 0.984375f;
        }
    }
}
