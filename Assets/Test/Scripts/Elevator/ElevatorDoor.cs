using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float currentTime = 0.0f, maxTime = 3.0f;
    [SerializeField] float distToOpen = 1.0f;
    [SerializeField] Vector3 basePos = Vector3.zero;
    [SerializeField] bool canMove = false;

    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        basePos = transform.position;
    }

    void Update()
    {
        Move();

        IncreaseTime();
    }

    void Move()
    {
        if (!canMove) return;
        
        transform.position = Vector3.Lerp(basePos, basePos + transform.forward * distToOpen, curve.Evaluate(currentTime / maxTime));
    }

    void IncreaseTime()
    {
        if (!canMove) return;

        currentTime += Time.deltaTime;

        if (currentTime > maxTime)
        {
            currentTime = 0.0f;
            canMove = false;
        }
    }
}
