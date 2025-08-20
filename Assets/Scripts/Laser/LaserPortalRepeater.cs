using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPortalRepeater : LaserBase
{
    LaserPortalRepeater otherPortal;
    bool isRepeating = false;
    protected override void Awake()
    {
        base.Awake();
        otherPortal = transform.GetComponent<Portal>().GetOtherPortal.transform.GetComponent<LaserPortalRepeater>();
        
    }
    public override void OnDetect()
    {
        isRepeating = true;
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
                if (/*!currCollide.IsInfiniteReflextion(this) &&*/ !currCollide.SearchDuplicatedSign(this) && currCollide != otherPortal)
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
            if (isRepeating) return;
            otherPortal.OnDetect();
            currCollide = otherPortal;
        }
        else
        {
            ChildLaserOff();
            if (!isRepeating)
            {
                ChildLaserOff();
                currCollide = null;
                otherPortal.OnLaserCollide(false);//혹시 클로저이슈?..
                return;
            }
            otherPortal.ChildLaserOff();
            otherPortal.currCollide = null;
            isRepeating = false;
        }
    }

}
