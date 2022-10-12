using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInput : MonoBehaviour
{
	[Header("Input Values")]
	public Vector2 move;
	public Vector2 look;
    public bool mouseL;

	public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

	public void OnLook(InputValue value)
    {
        MoveLook(value.Get<Vector2>());
    }

    public void OnMouseL(InputValue value)
    {
        MouseL(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void MoveLook(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void MouseL(bool newLookDirection)
    {
        mouseL = newLookDirection;
    }
}
