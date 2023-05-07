using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float sensitivity = 2f; // Mouse sensitivity for rotating the character
    public float walkSpeed = 5f; // Speed for walking forward and backward
    public float strafeSpeed = 3f; // Speed for strafing left and right

    private float yaw = 0f; // Current rotation of the character around the y-axis
    private float pitch = 0f; // Current rotation of the camera around the x-axis
    private bool isRotating = false; // Whether the character is currently being rotated with the mouse

    private Quaternion defaultRotation; // Default forward rotation of the camera
    private Quaternion currentRotation; // Current rotation of the camera

    [SerializeField]
    private Camera localCamera;

    [SerializeField]
    private GameObject hud;

    private void Start()
    {
        if(isLocalPlayer)
        {
            hud.SetActive(true);
        }

        if (localCamera.IsUnityNull())
            Debug.LogError($"localCamera of {gameObject.name} is not set");

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor at the start of the game
        defaultRotation = localCamera.transform.localRotation; // Store the default forward rotation of the camera
        currentRotation = defaultRotation; // Set the current rotation of the camera to the default forward rotation
    }

    private void Update()
    {
        // Check for right-click to start rotating the character
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window while rotating the character
        }

        // Check for right-click release to stop rotating the character
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor when the rotation is stopped
        }

        // Rotate the character with the mouse if right-click is held down
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
            localCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        // Rotate the camera left or right with the mouse if left-click is held down
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            currentRotation *= Quaternion.Euler(0f, mouseX, 0f);

        }

        // Interpolate the camera back to the default forward rotation when left-click is released
        if (Input.GetMouseButtonUp(0))
        {
            currentRotation = Quaternion.Lerp(currentRotation, defaultRotation, Time.deltaTime * 10f);
        }

        // Move the player forward, backward, left, or right while holding right-click and pressing WASD
       /* if (Input.GetMouseButton(1))
        {
            float forwardMovement = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime;
            float strafeMovement = Input.GetAxis("Horizontal") * strafeSpeed * Time.deltaTime;

            transform.Translate(strafeMovement, 0f, forwardMovement);
        }*/
    }
}