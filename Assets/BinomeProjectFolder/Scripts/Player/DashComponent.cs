using UnityEngine;
using UnityEngine.InputSystem;

public class DashComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;
    [SerializeField] AnimationCurve accelerationSpeedCurve = null;

    InputAction dashAction = null;

    [SerializeField] bool canDash = true;
    [SerializeField] bool isCharging = false;
    [SerializeField] bool isDashing = false;

    [SerializeField] float currentTime = 0.0f;
    [SerializeField] float maxTime = 3.0f;
    [SerializeField] float force = 10.0f;

    [SerializeField] float dashEndSpeedThreshold = 2.0f;
    [SerializeField] CameraShake cameraShake;

    public bool IsFullCharge() => currentTime >= maxTime;

    void Update()
    {
        UpdateCharge();

        if (isDashing)
        {
            float _currentSpeed = new Vector3(owner.Rigidbody.linearVelocity.x, 0, owner.Rigidbody.linearVelocity.z).magnitude;
            if (_currentSpeed <= dashEndSpeedThreshold)
            {
                EndDash();
            }
        }

        // Gestion du feedback visuel
        float _chargeRatio = currentTime / maxTime;
        if (cameraShake)
        {
            cameraShake.SetChargeRatio(_chargeRatio);

            if (IsFullCharge())
                cameraShake.StartShake(.4f);
            else
                cameraShake.StopShake();
        }
    }

    public void Init(InputAction _dashAction)
    {
        owner = GetComponent<Player>();
        dashAction = _dashAction;
        dashAction.performed += StartDash;
        dashAction.canceled += ReleaseDash;
    }

    void StartDash(InputAction.CallbackContext _context)
    {
        if (!canDash || isDashing) return;
        owner.Movement.SetCanMove(false);
        isCharging = true;
        currentTime = 0;
    }

    void ReleaseDash(InputAction.CallbackContext _context)
    {
        if (!isCharging) return;
        Dash(currentTime);
    }

    public void Dash(float _currentTime)
    {
        isCharging = false;
        isDashing = true;
        canDash = false;

        float _dashPower = force * EaseOutCirc(_currentTime / maxTime);

        Vector3 _dashDir = owner.transform.forward;
        Vector3 _velocity = owner.Rigidbody.linearVelocity;
        _velocity.y = 0f;
        owner.Rigidbody.linearVelocity = _velocity;

        owner.Rigidbody.AddForce(_dashDir * _dashPower, ForceMode.Impulse);
        currentTime = 0;
    }

    void EndDash()
    {
        isDashing = false;
        canDash = true;
        owner.Movement.SetCanMove(true);
    }

    void UpdateCharge()
    {
        if (!isCharging) return;

        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
            currentTime = maxTime;
    }

    float EaseOutCirc(float _time)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(_time - 1, 2));
    }
}
