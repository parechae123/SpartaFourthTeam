using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserIn : LaserBase
{

    public override void OnDetect()
    {
        
    }

    public override void OnLaserCollide(bool isLaserContact)
    {
        if (!isLaserContact)
        {
            Debug.Log("입력잃음");
        }
        else
        {
            //YOON : 문 처리
            Debug.Log("입력받음");

        }
    }
}
