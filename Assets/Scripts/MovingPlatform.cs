using UnityEngine;

public class MovingPlatform : MonoBehaviour, IMoveable
{
    [SerializeField] private float platformMoveSpeed;
    [SerializeField] private float travelDistance; //�÷����� �� �������� ������ �ִ� �Ÿ�
    [SerializeField] private Vector3 moveDirection = Vector3.forward;

    private Vector3 startPosition;

    public float moveSpeed { get; set; }

    private void Awake()
    {
        startPosition = transform.position;

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

        transform.Translate(movement);
    }

    private void OnTriggerStay(Collider other)
    {
        IMoveable moveableObject = other.GetComponent<IMoveable>();

        if (moveableObject != null)
        {
            moveableObject.OnMove(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
}