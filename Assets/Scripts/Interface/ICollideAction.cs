using UnityEngine;

public interface ICollideAction
{
    public Collider objectCollider { get; set; }
    public void OnCollide(Collider collider);
}