using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IObjectTrigger
{
    [SerializeField]private bool isActivated;
    public bool IsActivated { get { return isActivated; } }
    public ValueChangeFunc OnValueChanged { get; set; }
    public void ActivePlate(bool isPress)
    {
        if (IsActivated == isPress) return;

        isActivated = isPress;
        OnValueChanged?.Invoke(isPress);
    }
}
