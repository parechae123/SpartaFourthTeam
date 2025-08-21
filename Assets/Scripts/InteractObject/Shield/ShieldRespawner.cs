using UnityEngine;

public class ShieldRespawner : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    [SerializeField] private float minHeight = -15f;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (transform.position.y < minHeight)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}