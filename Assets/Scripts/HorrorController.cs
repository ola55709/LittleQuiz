using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class HorrorController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Look")]
    public Transform cameraTransform;
    public float mouseSensitivity = 0.08f; // New input mouse delta is big; small values feel right
    public float maxLookAngle = 80f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Move();
    }

    public void OnMove(InputAction.CallbackContext ctx) {moveInput = ctx.ReadValue<Vector2>();}
    public void OnLook(InputAction.CallbackContext ctx) { lookInput = ctx.ReadValue<Vector2>(); }

    void Move()
    {
        Vector3 move = (transform.right * moveInput.x + transform.forward * moveInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void Look()
    {
        // Look input is mouse delta per frame
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}