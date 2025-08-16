using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, ICollideAction
{
    [SerializeField] private float jumpForce;
    public Collider objectCollider { get; set; }

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

        Vector3 launchDirection = new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized;

        // �÷��̾ ���� ������ ���, ���� �������θ� ���� ����
        if (launchDirection == Vector3.zero)
        {
            launchDirection = Vector3.up;
        }
        else
        {
            launchDirection = (launchDirection + Vector3.up * 0.5f).normalized;
        }

        //rb.velocity = Vector3.zero;
        rb.AddForce(launchDirection * jumpForce, ForceMode.Impulse);
    }
}
