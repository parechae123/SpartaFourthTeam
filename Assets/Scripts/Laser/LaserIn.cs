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
            Debug.Log("�Է�����");
        }
        else
        {
            //YOON : �� ó��
            Debug.Log("�Է¹���");

        }
    }
}
