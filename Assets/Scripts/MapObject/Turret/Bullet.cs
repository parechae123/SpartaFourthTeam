using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour, ICollideAction
{
    //총알 속도,데미지,지속시간 설정
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float knockbackForce = 80f;

    [Header("Layer Settings")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private LayerMask blockMask;
    [SerializeField] private Transform ignoredRoot;

    private Rigidbody rb;
    private Poolable poolable;
    private float despawnTime;
    private bool hasHit;
    public Collider objectCollider { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        poolable = GetComponent<Poolable>();
        objectCollider = GetComponent<Collider>();

        // 총알 기본값 설정
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void OnEnable()
    {
        // 활성화될 때마다 초기화
        hasHit = false;
        despawnTime = Time.time + lifeTime;
    }

    private void Update()
    {
        if (Time.time >= despawnTime)
            Despawn();
    }
    private void Despawn()
    {
        if (poolable != null) poolable.Despawn(); //총알이 풀에서 나온거면 반환
    }

    public void Fire(Vector3 position, Vector3 direction, float overrideSpeed = -1f, Transform ignored = null)
    {
        transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction, Vector3.up)); // 총알을 지정 위치·방향에 배치
        rb.velocity = direction.normalized * (overrideSpeed > 0f ? overrideSpeed : speed); // 속도를 부여

        ignoredRoot = ignored;// 발사자 무시 설정
        hasHit = false; // 충돌 여부 초기화
        despawnTime = Time.time + lifeTime;// 수명 만료 시각 지정
    }

    public void OnCollide(Collider other)
    {
        if (hasHit || other == null) return;

        // 자기 편/무시 대상 충돌 처리 안하도록
        if (ignoredRoot != null && other.transform.IsChildOf(ignoredRoot)) return;

        // 레이어 마스크 필터
        int playerLayer = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer != playerLayer) return;


        //넉백 처리
        var targetRb = other.attachedRigidbody ?? other.GetComponentInParent<Rigidbody>();
        if (targetRb != null && !targetRb.isKinematic)
        {
            Vector3 dir = (rb != null && rb.velocity.sqrMagnitude > 0.0001f)
                        ? rb.velocity.normalized
                        : (targetRb.worldCenterOfMass - transform.position).normalized;

            targetRb.AddForce(dir * knockbackForce, ForceMode.VelocityChange);
        }

        hasHit = true; //중복타격 방지
        Despawn(); //총알 풀로 반환
    }

    private void OnTriggerEnter(Collider other)
    {
        // blockMask에 포함된 레이어인지 검사
        if (((1 << other.gameObject.layer) & blockMask.value) != 0)
        {
            Despawn();
            return;
        }

        OnCollide(other);
    }
}
