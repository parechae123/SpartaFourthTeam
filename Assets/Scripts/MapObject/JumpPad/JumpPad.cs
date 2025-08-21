using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, ICollideAction
{
    [SerializeField] private float jumpForce;
    public Collider objectCollider { get; set; }
    [SerializeField] private Vector3 forceDirection = Vector3.up;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip jumpSound;
    private AudioSource audioSource;

    private void Awake()
    {
        forceDirection = forceDirection.normalized;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
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

        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
}
