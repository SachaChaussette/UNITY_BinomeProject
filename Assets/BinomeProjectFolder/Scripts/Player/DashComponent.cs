using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DashComponent : MonoBehaviour
{
    [SerializeField] Player owner = null;
    [SerializeField] AnimationCurve accelerationSpeedCurve = null;

    InputAction dashAction = null;

    [SerializeField] bool canDash = true, isCharging = false;
    [SerializeField] float currentTime = 0.0f, maxTime = 3.0f;
    [SerializeField] float force = 10.0f;
    [SerializeField] CameraShake cameraShake;



    public bool IsFullCharge()
    {
        return currentTime >= maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        float _chargeRatio = currentTime / maxTime;
        if (!cameraShake) return;
        cameraShake.SetChargeRatio(_chargeRatio);

        if (IsFullCharge())
        {
            cameraShake.StartShake(.4f);
        }
        else
        {
            cameraShake.StopShake();
        }
    }

    public void Init(InputAction _dashAction)
    {
        owner = GetComponent<Player>();
        dashAction = _dashAction;
        dashAction.performed += StartDash;
        dashAction.canceled += RealisedDash;
    }

    void StartDash(InputAction.CallbackContext _context)
    {
        if (!canDash) return;
        owner.Movement.SetCanMove(false);
        isCharging = true;
        currentTime = 0;
    }

    void RealisedDash(InputAction.CallbackContext _context)
    {
        Dash(currentTime);
    }

    public void Dash(float _currentTime)
    {
        owner.Movement.SetCanMove(true);
        isCharging = false;

        owner.Rigidbody.AddForce(owner.transform.forward * (force * EaseOutCirc(_currentTime / maxTime)), ForceMode.Impulse);
        currentTime = 0;
    }

    void UpdateTime()
    {
        if (!isCharging) return;
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            currentTime = maxTime;
        }
    }

    public float EaseOutCirc(float _time)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(_time - 1, 2));
    }
}
