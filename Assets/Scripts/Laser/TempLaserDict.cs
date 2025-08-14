using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager로 이전해야될듯
/// </summary>
public class TempLaserDict : SingleTon<TempLaserDict>
{
    Dictionary<Collider, ILaserCollide> laserCollides;
    public Dictionary<Collider, ILaserCollide> GetLaserCollide { get { return laserCollides; } }

    protected override void Init()
    {
        base.Init();
        laserCollides = new Dictionary<Collider, ILaserCollide>();
    }
    protected override void Reset()
    {
        laserCollides.Clear();
    }
    public void RegistLaserOBJ(Collider laserColl, LaserBase laserOBJ)
    {
        laserCollides.Add(laserColl,laserOBJ);
    }
    public void RemoveLaserOBJ(Collider laserColl)
    {
        laserCollides.Remove(laserColl);
    }

}
public class SingleTon <T> where T : SingleTon<T>, new()
{
    private static T instance;
    public static T GetInstance 
    { 
        get 
        { 
            if(instance == null)
            {
                instance = new T();
                instance.Init();
            }
            return instance;
        } 
    }
    protected virtual void Init()
    {

    }
    /// <summary>
    /// 추후 씬 전환시 gameobject와 관련된 값 비어줄때 사용
    /// </summary>
    protected virtual void Reset()
    {

    }
}
