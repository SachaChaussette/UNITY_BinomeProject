using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] float shakeMagnitude = 0.4f;
    [SerializeField] float returnSpeed = 10f;

    [Header("Charge Settings")]
    [SerializeField] float maxChargeOffset = 1.0f; // distance maximale de recul
    [SerializeField] float chargeBackSpeed = 2.0f;

    Vector3 originalPos;
    bool isShaking = false;
    float currentChargeRatio = 0f;

    void Awake()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        Shake();
    }

    public void SetChargeRatio(float _ratio)
    {
        currentChargeRatio = Mathf.Clamp01(_ratio);
    }

    public void StartShake(float _magnitude)
    {
        shakeMagnitude = _magnitude;
        isShaking = true;
    }

    public void StopShake()
    {
        isShaking = false;
    }

    void Shake()
    {
        if (isShaking)
        {
            float _x = Random.Range(-1f, 1f) * shakeMagnitude;
            float _y = Random.Range(-1f, 1f) * shakeMagnitude;

            Vector3 _shakeOffset = new Vector3(_x, _y, 0);
            transform.localPosition = originalPos + _shakeOffset + Vector3.back * (maxChargeOffset);
        }
        else
        {
            Vector3 _targetPos = originalPos + Vector3.back * (maxChargeOffset * currentChargeRatio);
            transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, Time.deltaTime * chargeBackSpeed);
        }
    }
}
