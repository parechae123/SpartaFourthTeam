using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflecter : LaserBase,IGrabable
{
    bool isContacted = false;
    public void OnGrabEnter()
    {
        //transform.parent = 플레이어Transform;
        //transform.localPosition = 플레이어Transform.forward;
    }
    public void OnGrabExit()
    {
        transform.parent = null;
    }
    public override void OnDetect()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, float.PositiveInfinity, searchLayer))
        {
            SetLine(hit.distance);
            if (TempLaserDict.GetInstance.GetLaserCollide.ContainsKey(hit.collider))
            {
                if (currCollide != TempLaserDict.GetInstance.GetLaserCollide[hit.collider]) currCollide = TempLaserDict.GetInstance.GetLaserCollide[hit.collider];
                
                if (!currCollide.IsInfiniteReflextion(this))
                {
                    TempLaserDict.GetInstance.GetLaserCollide[hit.collider].OnLaserCollide(true);
                }
                else
                {
                    SetLine(hit.distance);
                }
            }
        }
        else
        {
            if (currCollide != null)
            {
                currCollide.OnLaserCollide(false);
                currCollide = null;
            }
            SetLine(3000f);
        }
    }
    public override void OnLaserCollide(bool isLaserContact)
    {
        if (isLaserContact) OnDetect();
        else
        {
            ChildLaserOff();
        }
    }
    public override bool IsInfiniteReflextion(ILaserCollide laser)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, float.PositiveInfinity, searchLayer))
        {
            TempLaserDict.GetInstance.GetLaserCollide.TryGetValue(hit.collider, out ILaserCollide compare);
            if (compare == laser)
            {
                return true;
            }
        }
        return false;
    }
}
