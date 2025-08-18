using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;

    // 문을 여는 속도
    [SerializeField] private float openSpeed = 1f;

    private bool isOpened = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = closedPosition;
        transform.localPosition = closedPosition;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, openSpeed * Time.deltaTime);
    }

    public void Open()
    {
        if (!isOpened)
        {
            isOpened = true;
            targetPosition = openPosition;
        }
    }

    public void Close()
    {
        if (isOpened)
        {
            isOpened = false;
            targetPosition = closedPosition;
        }
    }
}