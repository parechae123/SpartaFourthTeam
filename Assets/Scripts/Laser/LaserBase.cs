using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public abstract class LaserBase : MonoBehaviour, IDetectAction, ILaserCollide
{
    protected LineRenderer line;
    protected LayerMask searchLayer = 0;
    protected Ray ray;
    protected ILaserCollide currCollide;
    protected virtual void Awake()
    {
        if (line == null) line = GetComponent<LineRenderer>();
        searchLayer += 1 << LayerMask.NameToLayer("LaserObjects");
        TempLaserDict.GetInstance.RegistLaserOBJ(GetComponent<Collider>(), this);
    }
    public virtual void OnDetect()
    {
        
    }
    public abstract void OnLaserCollide(bool isLaserContact);

    public void ChildLaserOff()
    {
        if(line != null)OnLaserRendering(0f);

        if(currCollide != null && currCollide != this) currCollide.ChildLaserOff();
        currCollide = null;
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

    public virtual bool SearchDuplicatedSign(ILaserCollide target)
    {
        if (currCollide == null) return false;
        else if (currCollide == target) return true;
        else if (target == this) return false;//«—∑Á«¡ µ∫

        return (currCollide.SearchDuplicatedSign(target));
    }
}
