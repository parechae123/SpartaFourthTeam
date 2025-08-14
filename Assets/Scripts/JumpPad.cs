using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour , ICollideAction
{
    [SerializeField] private float jumpForce;

    protected Collider collider { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            OnCollide(other);
        }
    }

    public void OnCollide(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
