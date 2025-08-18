using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public abstract class LaserBase : MonoBehaviour, IDetectAction, ILaserCollide
{
    protected LineRenderer line;
    protected LayerMask searchLayer = 0;
    protected Ray ray;
    protected ILaserCollide currCollide;
    void Awake()
    {
        if (line == null) line = GetComponent<LineRenderer>();
        searchLayer += 1 << 13;
        TempLaserDict.GetInstance.RegistLaserOBJ(GetComponent<Collider>(), this);
    }
    public virtual void OnDetect()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, float.PositiveInfinity, searchLayer))
        {
            OnLaserRendering(hit.distance);
            if (TempLaserDict.GetInstance.GetLaserCollide.ContainsKey(hit.collider))
            {
                if(currCollide != TempLaserDict.GetInstance.GetLaserCollide[hit.collider]) currCollide = TempLaserDict.GetInstance.GetLaserCollide[hit.collider];
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
    public abstract void OnLaserCollide(bool isLaserContact);

    public virtual bool IsInfiniteReflextion(ILaserCollide laser)
    {
        return false;
    }

    public void ChildLaserOff()
    {
        if(line != null)OnLaserRendering(0f);

        if(currCollide != null) currCollide.ChildLaserOff();


    }
    public void OnLaserRendering(float dist)
    {
        if (dist == 0f) line.enabled = false;
        else
        {
            line.enabled = true;
            line.SetPositions(new Vector3[2] { transform.position, transform.position + (transform.forward * dist) });
        }
    }
}
