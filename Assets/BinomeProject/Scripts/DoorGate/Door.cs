using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Vector3 startPos = Vector3.zero, endPos = Vector3.zero;
    [SerializeField] float currentTime = 0.0f, maxTime = 3.0f;
    [SerializeField] bool canMove = false, mustClose = false, isOpen = false;
    [SerializeField] DoorsDetection doorDetection;
    [SerializeField] float distanceToOpen = 1.0f;

    void Start()
    {
        if (!doorDetection) return;

        startPos = transform.position;
        endPos = startPos + transform.right * distanceToOpen;

        doorDetection.OnPlayerNear += OnPlayerNear;
    }

    void OnPlayerNear(bool _isPlayerNear)
    {
        canMove = true;

        mustClose = !_isPlayerNear;
    }

    void Update()
    {
        if (!canMove) return;

        IncreaseTime();

        float _time = EaseOutBounce(currentTime / maxTime);

        transform.position = Vector3.Lerp(startPos, endPos, _time);
    }

    void IncreaseTime()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0.0f;
            isOpen = !isOpen;
            SwitchVector(ref startPos, ref endPos);
            canMove = false;
            MustCloseDoor();
        }
    }

    void MustCloseDoor()
    {
        if (!mustClose || !isOpen) return;
        canMove = true;
        mustClose = false;
    }

    void SwitchVector(ref Vector3 _start, ref Vector3 _end)
    {
        Vector3 _temp = _start;
        _start = _end;
        _end = _temp;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPos, 0.1f);
        Gizmos.color = Color.white;
    }
}
