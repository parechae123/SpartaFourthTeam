using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserIn : LaserBase , IObjectTrigger
{
    private bool isActivated;
    public bool IsActivated { get { return isActivated; } }
    public ValueChangeFunc OnValueChanged { get; set; }

    public override void OnDetect()
    {
        
    }

    public override void OnLaserCollide(bool isLaserContact)
    {
        if (IsActivated == isLaserContact) return;
        isActivated = isLaserContact;
        OnValueChanged?.Invoke(isLaserContact);
    }
}
