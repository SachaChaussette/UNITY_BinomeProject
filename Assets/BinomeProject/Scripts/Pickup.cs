using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] float currentTime = 0.0f, maxTime = 2.0f;
    [SerializeField] float floatingHeight = 0.5f;
    [SerializeField] bool isClockwise = false;
    [SerializeField] AnimationCurve curve = new AnimationCurve();
    Vector3 basePos = Vector3.zero;

    void Start()
    {
        basePos = transform.position;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > maxTime)
        {
            currentTime = 0.0f;
            isClockwise = !isClockwise;
            basePos = transform.position;
            floatingHeight *= -1.0f;
        }

        float _factor = isClockwise ? 1.0f : -1.0f;

        float angle = EaseInBounce(currentTime / maxTime) * 360.0f * _factor;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        float _time = curve.Evaluate(currentTime / maxTime);
        transform.position = Vector3.Lerp(basePos, basePos + Vector3.up * floatingHeight, _time);
    }

    float EaseInBounce(float _t)
    {
        return 1 - EaseOutBounce(1 - _t);
    }

    float EaseOutBounce(float _t)
    {
        const float _n1 = 7.5625f;
        const float _d1 = 2.75f;

        if (_t < 1.0f / _d1) 
        {
            return _n1* _t * _t;
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
