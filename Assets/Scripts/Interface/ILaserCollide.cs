using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ILaserCollide
{
    public void OnLaserCollide(bool isLaserContact);
    public void OnLaserRendering(float dist);
    public bool IsInfiniteReflextion(ILaserCollide prev);
    public void ChildLaserOff();
}
