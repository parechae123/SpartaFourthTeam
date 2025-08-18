using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ICollideAction collideAction = other.GetComponent<ICollideAction>();
        if (collideAction != null)
        {
            collideAction.OnCollide(this.GetComponent<Collider>());
        }
    }
}
