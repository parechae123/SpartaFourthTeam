using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflecter : LaserBase,IGrabable
{
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
            OnLaserRendering(hit.distance);
            if (TempLaserDict.GetInstance.GetLaserCollide.ContainsKey(hit.collider))
            {
                if (currCollide != null && currCollide != TempLaserDict.GetInstance.GetLaserCollide[hit.collider])
                {
                    currCollide.ChildLaserOff();
                    currCollide.OnLaserCollide(false);
                }
                currCollide = TempLaserDict.GetInstance.GetLaserCollide[hit.collider];
                if (/*!currCollide.IsInfiniteReflextion(this) &&*/ !currCollide.SearchDuplicatedSign(this))
                {
                    currCollide.OnLaserCollide(true);
                }
                else
                {
                    OnLaserRendering(hit.distance);
                    //currCollide.OnLaserCollide(false);
                    currCollide = null;
                }
            }
            return;
        }
        if (currCollide != null)
        {
            currCollide.OnLaserCollide(false);
            currCollide = null;
        }
        OnLaserRendering(3000f);
    }
    public override void OnLaserCollide(bool isLaserContact)
    {
        if (isLaserContact && !SearchDuplicatedSign(this)) 
        { 
            OnDetect(); 
        }
        else
        {
            ChildLaserOff();
        }
    }
}
