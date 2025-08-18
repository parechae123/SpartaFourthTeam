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

    private void OnCollisionEnter(Collision collision)
    {
        PresurePlate(collision.collider,true);
    }

    private void OnCollisionExit(Collision collision)
    {
        PresurePlate(collision.collider,false);
    }

    public void PresurePlate(Collider collider,bool isEnter)
    {
        if(collider.TryGetComponent<PressurePlate>(out PressurePlate result))
        {
            result.ActivePlate(isEnter);
        }
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
