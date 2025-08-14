using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour , IGrabable,ICollideAction
{
    [SerializeField] Rigidbody rb;
    Collider col;
    public Collider objectCollider { get => col; set => col= value; }
    private void Start()
    {
        col = GetComponent<Collider>();
    }
    public void OnCollide(Collider collider)
    {
        /*collider.GetComponents<PressurePlate>().SetValue*/
        //
    }

    public void OnGrabEnter()
    {
        rb.isKinematic = true;
        //transform.parent = �÷��̾�Transform;
        //transform.localPosition = �÷��̾�Transform.forward;
    }
    public void OnGrabExit()
    {
        rb.isKinematic = false;
        transform.parent = null;
    }
    
}
