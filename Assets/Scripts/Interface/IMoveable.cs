
using UnityEngine;

public interface IMoveable
{
    public float moveSpeed {  get; set; }
    public void OnMove(Vector3 movement);
}
