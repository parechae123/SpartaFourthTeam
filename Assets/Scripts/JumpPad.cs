using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, ICollideAction
{
    [SerializeField] private float jumpForce;
    public Collider objectCollider { get; set; }
    [SerializeField] private Vector3 forceDirection = Vector3.up;

    /*
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            OnCollide(other);
        }
    }
    */

    private void Awake()
    {
        forceDirection = forceDirection.normalized;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            OnCollide(collision.collider);
        }
    }

    public void OnCollide(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null) return;

        //rb.velocity = Vector3.zero;
        rb.AddForce(forceDirection * jumpForce, ForceMode.Impulse);
    }
}
