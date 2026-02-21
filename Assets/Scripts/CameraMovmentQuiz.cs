using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovmentQuiz : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float sensitivity = 0.15f;
    [SerializeField]
    private Vector2 xLimit = new Vector2(-40f, 80f);
    [SerializeField]
    private Vector2 yLimit = new Vector2(-90f, 90f);

    private float _rotX;
    private float _rotY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 currentRot = transform.localRotation.eulerAngles;
        _rotY = currentRot.y;
        _rotX = currentRot.x;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        _rotY += mouseDelta.x * sensitivity;
        _rotX -= mouseDelta.y * sensitivity;

        _rotX = Mathf.Clamp(_rotX, xLimit.x, xLimit.y);

        _rotY = Mathf.Clamp(_rotY, yLimit.x, yLimit.y);

        transform.localRotation = Quaternion.Euler(_rotX, _rotY, 0f);
    }
}
