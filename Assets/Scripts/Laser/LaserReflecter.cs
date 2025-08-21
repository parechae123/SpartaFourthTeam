using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflecter : LaserBase,IGrabable
{
    [SerializeField] Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        if (rb == null) rb = rb.GetComponent<Rigidbody>();
    }
    public void OnGrabEnter()
    {
        rb.isKinematic = true;
        Transform playerTransform = PlayerManager.Instance.player.transform;
        transform.SetParent(playerTransform, false);
        transform.position = playerTransform.position + playerTransform.forward * (1.5f) + Vector3.up;
    }
    public void OnGrabExit()
    {
        rb.isKinematic = false;
        transform.SetParent(null);
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
