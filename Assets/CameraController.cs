using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float scrollSpeedMultiplier = 5f;
    public float minSpeed = 1f;
    public float maxSpeed = 100f;
    public float shiftMultiplier = 3f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public float maxPitch = 85f;

    float yaw;
    float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 euler = transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;
    }

    void LateUpdate()
    {
        HandleMouseLook();
        HandleMovement();
        HandleScrollSpeed();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        float y = 0f;
        if (Input.GetKey(KeyCode.E)) y += 1f;
        if (Input.GetKey(KeyCode.Q)) y -= 1f;

        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= shiftMultiplier;

        Vector3 direction =
            transform.right * x +
            transform.forward * z +
            transform.up * y;

        transform.position += direction * speed * Time.deltaTime;
    }

    void HandleScrollSpeed()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            moveSpeed = Mathf.Clamp(
                moveSpeed + scroll * scrollSpeedMultiplier,
                minSpeed,
                maxSpeed
            );
        }
    }
}
