using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour , IGrabable,ICollideAction
{
    [SerializeField] Rigidbody rb;

    public Collider objectCollider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void OnCollide(Collider collider)
    {
        /*collider.GetComponents<PressurePlate>().SetValue*/
    }

    public void OnGrabEnter()
    {
        //transform.parent = �÷��̾�Transform;
        //transform.localPosition = �÷��̾�Transform.forward;
    }
    public void OnGrabExit()
    {
        transform.parent = null;
    }
    
}
