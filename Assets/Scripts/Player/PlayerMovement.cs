using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private Joystick joystick;

    [Header("Movement Boundaries")]
    [SerializeField]
    private float minX = -8f;

    [SerializeField]
    private float maxX = 8f;

    [SerializeField]
    private float minZ = -4f;

    [SerializeField]
    private float maxZ = 4f;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Vector2 keyboardInput =
            inputActions.Player.Move.ReadValue<Vector2>();

        Vector2 joystickInput = Vector2.zero;

        if (joystick != null)
        {
            joystickInput = joystick.Direction;
        }

        Vector2 input = keyboardInput;

        if (joystickInput.sqrMagnitude > 0.01f)
        {
            input = joystickInput;
        }

        Vector3 movement = new Vector3(input.x, 0f, input.y);

        if (movement.sqrMagnitude > 1f)
        {
            movement.Normalize();
        }

        // Move player
        transform.position +=  movement * moveSpeed * Time.deltaTime;

        // Keep player inside the playable area
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        transform.position = position;
    }
}