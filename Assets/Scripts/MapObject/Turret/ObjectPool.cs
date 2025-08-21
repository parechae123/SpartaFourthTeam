using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject prefab; //총알
    [SerializeField] private int initialSize = 20; //초기 개수
    [SerializeField] private Transform container;   // 풀 인스턴스들을 담을 부모(없으면 자기 자신)

    private readonly Queue<GameObject> pool = new Queue<GameObject>(); //비활성화된 오브젝트 저장용 큐

    private void Awake()
    {
        if (container == null) container = transform;
        if (prefab == null)
        {
            Debug.LogError("프리팹이 없습니다");
            return;
        }
        Prewarm();
    }

    private void Prewarm() // 초기 개수만큼 미리 오브젝트 생성해서 큐에보관
    {
        for (int i = 0; i < Mathf.Max(0, initialSize); i++)
            pool.Enqueue(CreateInstance());
    }

    private GameObject CreateInstance()//프리팹을 새로 생성하는 함수
    {
        //생성 후 비활성화
        var go = Instantiate(prefab, container);
        go.SetActive(false);

        var poolable = go.GetComponent<Poolable>();
        if (poolable == null) poolable = go.AddComponent<Poolable>();
        poolable.SetOwner(this);

        return go;
    }

    //풀에서 오브젝트 하나 꺼내는 함수
    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            pool.Enqueue(CreateInstance());
        }

        var go = pool.Dequeue();
        go.SetActive(true);
        return go;
    }

    //오브젝트를 풀에 반환하는 함수
    public void Return(GameObject go)
    {
        if (go == null) return;
        go.SetActive(false);
        pool.Enqueue(go);
    }
}
