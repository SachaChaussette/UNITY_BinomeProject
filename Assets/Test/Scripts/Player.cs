using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(InputComponent))]
public class Player : MonoBehaviour
{
	[SerializeField] MovementComponent movement = null;
	[SerializeField] InputComponent inputs = null;

    void Start()
	{
		movement = GetComponent<MovementComponent>();
        inputs = GetComponent<InputComponent>();

		movement.Init(inputs.MoveAction, inputs.RotateAction, inputs.JumpAction);
	}
}