using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    public Transform respawnPoint;

    
    public float minHeight = -15f;

    void Update()
    { 
        if (transform.position.y < minHeight)
        {
            Respawn();
        }
    }
    void Respawn()
    {
        transform.position = respawnPoint.position;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}