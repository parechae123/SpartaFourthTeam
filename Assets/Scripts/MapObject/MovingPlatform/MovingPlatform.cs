using UnityEngine;

public class MovingPlatform : MonoBehaviour, IMoveable
{
    [SerializeField] private float platformMoveSpeed;
    [SerializeField] private float travelDistance; //플랫폼이 한 방향으로 움직일 최대 거리
    [SerializeField] private Vector3 moveDirection = Vector3.forward;

    private Vector3 startPosition;
    private Rigidbody platformBody;

    public float moveSpeed { get; set; }

    private void Awake()
    {
        platformBody = GetComponent<Rigidbody>();

        startPosition = platformBody.position;

        moveSpeed = platformMoveSpeed;
    }


    private void FixedUpdate()
    {
        OnMove(moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(Vector3 movement)
    {
        if (Vector3.Distance(startPosition, transform.position) >= travelDistance)
        {
            moveDirection = -moveDirection;
            startPosition = transform.position;
        }
        platformBody.MovePosition(transform.position + movement);
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.rigidbody;
        if (rb != null)
        {
            other.transform.SetParent(transform);
        }
    }


    private void OnCollisionExit(Collision other)
    {
        Rigidbody rb = other.rigidbody;
        if (rb != null)
        {
            other.transform.SetParent(null);
        }
    }
}