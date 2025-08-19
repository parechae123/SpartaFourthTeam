using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IObjectTrigger
{
    [SerializeField]private bool isActivated;
    public bool IsActivated { get { return isActivated; } }
    public ValueChangeFunc OnValueChanged { get; set; }
    public LayerMask layers;


    private void OnCollisionEnter(Collision collision)
    {
        PresurePlate(collision.collider, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        PresurePlate(collision.collider, false);
    }

    public void PresurePlate(Collider collider, bool isEnter)
    {
        if ((layers & (1 << collider.gameObject.layer)) != 0)
        {
            ActivePlate(isEnter);
        }
    }
    public void ActivePlate(bool isPress)
    {
        if (IsActivated == isPress) return;

        isActivated = isPress;
        OnValueChanged?.Invoke(isPress);
    }
}
