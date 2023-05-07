using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 10f;

    [SerializeField] private GameObject localCamera;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject canvas;


    private Rigidbody rb;

    private const float animationThreshold = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isLocalPlayer)
        {
            localCamera.SetActive(true);
            GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            canvas.SetActive(true);
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isWalking = Mathf.Abs(horizontal) > animationThreshold || Mathf.Abs(vertical) > animationThreshold;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;

        Vector3 movement = Vector3.zero;

        if (isWalking)
        {
            movement = new Vector3(horizontal, 0f, vertical) * (isRunning ? sprintSpeed : moveSpeed) * Time.deltaTime;
            transform.Translate(movement, Space.Self);
        }

        animator.SetFloat("ForwardSpeed", isWalking ? (isRunning ? 2f : 1f) * Mathf.Sign(vertical) : 0f);
        animator.SetFloat("Direction", isWalking ? (isRunning ? 2f: 1f) * horizontal : 0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}