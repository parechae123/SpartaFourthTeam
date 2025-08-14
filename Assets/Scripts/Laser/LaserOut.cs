using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOut : LaserBase
{
    public override void OnDetect()
    {
        base.OnDetect();
    }

    // Update is called once per frame
    void Update()
    {
        OnDetect();
    }
    public override void OnLaserCollide(bool isLaserContact)
    {
        
    }

}
