using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour , IGrabable
{
    [SerializeField] Rigidbody rb;
    Collider col;
    public Collider objectCollider { get => col; set => col= value; }
    private void Start()
    {
        col = GetComponent<Collider>();
    }

    public void OnGrabEnter()
    {
        rb.isKinematic = true;
        Transform playerTransform = PlayerManager.Instance.player.transform;
        transform.SetParent(playerTransform, false);
        transform.position = playerTransform.position + playerTransform.forward * (1.5f) + Vector3.up;
    }
    public void OnGrabExit()
    {
        rb.isKinematic = false;
        transform.SetParent(null);
    }
    
}
