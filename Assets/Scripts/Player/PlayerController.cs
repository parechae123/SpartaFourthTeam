using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("Grab")]
    [SerializeField] Player player;
    [SerializeField] float grabDistance;
    const float UPDATEDELAY = 0.05f;
    float lastUpdate = 0.0f;
    const float DETECTDISTANCE = 1.0f;
    Camera cam;

    [Header("CrossHair")]
    [SerializeField] Transform UIParent;
    [SerializeField] Sprite crosshairImg;
    RectTransform crosshair;
    Vector3 midPos;

    [Header("Interactables")]
    bool isGrab = false;
    [SerializeField] LayerMask itemLayer;
    IGrabable currentGrabable = null;


    //Other not shown in Inspector
    [HideInInspector] Rigidbody rb;
    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            throw new System.Exception("Player doesn't have rigidbody");
        crosshair = new GameObject("CrossHairUI").AddComponent<RectTransform>();
        crosshair.transform.SetParent(UIParent);
        crosshair.gameObject.AddComponent<Image>().sprite = crosshairImg;
        crosshair.gameObject.GetComponent<Image>().color = Color.red;
        crosshair.localScale = Vector2.one * 0.2f;
        crosshair.anchoredPosition = Vector3.zero;
        midPos = new Vector3(Screen.width / 2, Screen.height / 2);
        player = this.GetComponent<Player>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        OnMove(currentMoveInput);
    }

    private void Update()
    {
        if (Time.time - lastUpdate < UPDATEDELAY)
            return;
        if (isGrab)
        {
            return;
        }
        lastUpdate = Time.time;
        Ray ray = cam.ScreenPointToRay(midPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, DETECTDISTANCE, itemLayer))
        {
            if (hit.collider.gameObject.TryGetComponent<IGrabable>(out IGrabable grabtarget))
            {
                if (grabtarget != currentGrabable)
                {
                    currentGrabable = grabtarget;
                }
            }
            else
                currentGrabable = null;
        }
        else
        {
            currentGrabable = null;
        }
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
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //If current Interactable is not null, Grab
            if (currentGrabable != null && !isGrab)
            {
                isGrab = true;
                currentGrabable.OnGrabEnter();
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if (isGrab)
            {
                //End Player Grab mode
                currentGrabable.OnGrabExit();
                currentGrabable = null;
                isGrab = false;
            }
        }
    }

    //inputActions End

    //Move
    public void OnMove(Vector3 movement)
    {
        Vector3 curdir = transform.forward * movement.y + transform.right * movement.x;
        curdir = curdir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + curdir);
    }
    //Check if Player is Grounded
    public bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.05f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.05f), Vector3.down)
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
