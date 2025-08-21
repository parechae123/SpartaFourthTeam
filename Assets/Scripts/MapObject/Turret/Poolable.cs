using UnityEngine;

public class Poolable : MonoBehaviour
{
    private ObjectPool owner;

    public void SetOwner(ObjectPool pool) => owner = pool;

    public void Despawn()
    {
        if (owner != null) owner.Return(gameObject);
        else Destroy(gameObject); // 풀 말고 추가로 생성시 파괴
    }
}
