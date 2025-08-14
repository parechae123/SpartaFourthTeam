using UnityEngine;

public interface ICollideAction
{
    protected Collider collider { get; set; }
    public void OnCollide(Collider collider);
}
