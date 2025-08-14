using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMoveable
{
    [Header("이동")]
    [SerializeField] float baseMovementSpeed;
    public float speedMultiplier = 1.0f;
    public float moveSpeed { get { return baseMovementSpeed * speedMultiplier; } set { return; } }
    Vector2 currentMoveInput = Vector2.zero;

    [Header("점프")]
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float jumpForce;

    [Header("Look")]
    [SerializeField] Transform cameraContainer;
    [SerializeField] float minLook;
    [SerializeField] float maxLook;
    float cameraXRotation;
    [SerializeField] float lookSensitivity;
    Vector2 mouseDelta;

    [Header("인벤토리")]
    bool isInventoryVisible = false;


    //Other not shown in Inspector
    [HideInInspector] Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            throw new System.Exception("Player doesn't have rigidbody");
    }

    private void FixedUpdate()
    {
        OnMove(currentMoveInput);
    }

    private void LateUpdate()
    {
        OnLook();
    }

    //inputActions

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            currentMoveInput = context.ReadValue<Vector2>();
        else if (context.phase == InputActionPhase.Canceled)
            currentMoveInput = Vector2.zero;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnInventoryToggle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isInventoryVisible = !isInventoryVisible;
            GetComponent<Player>().playerInventory.ActivateUI(isInventoryVisible);
        }
    }

    //inputActions End

    //Move
    public void OnMove(Vector3 movement)
    {
        Vector3 curdir = transform.forward * currentMoveInput.y + transform.right * currentMoveInput.x;
        curdir = curdir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + curdir);
    }
    //Check if Player is Grounded
    public bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
                return true;
        }
        return false;
    }
    //Look and Set Camera
    void OnLook()
    {
        cameraXRotation += mouseDelta.y * lookSensitivity;
        cameraXRotation = Mathf.Clamp(cameraXRotation, minLook, maxLook);
        cameraContainer.localEulerAngles = new Vector3(-cameraXRotation, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
}
