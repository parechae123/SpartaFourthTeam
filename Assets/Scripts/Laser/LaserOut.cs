using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOut : LaserBase
{
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

                TempLaserDict.GetInstance.GetLaserCollide[hit.collider].OnLaserCollide(true);
            }
        }
        else
        {
            if (currCollide != null)
            {
                currCollide.OnLaserCollide(false);
                currCollide = null;
            }
            OnLaserRendering(3000f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnDetect();
    }
    public override void OnLaserCollide(bool isLaserContact)
    {
        
    }
    public override bool SearchDuplicatedSign(ILaserCollide next)
    {
        return false;
    }
}
