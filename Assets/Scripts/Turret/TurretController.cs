using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TurretDetector detector;
    [SerializeField] private Transform yawPivot;       // 좌우 회전 축
    [SerializeField] private Transform bulletSpawn;    // 총알 스폰 위치
    [SerializeField] private ObjectPool bulletPool;    // 오브젝트 풀

    [Header("Aiming")]
    [SerializeField] private float turnSpeedDeg = 360f; // 초당 회전 속도

    [Header("Fire")]
    [SerializeField] private float fireRate = 5f;     // 초당 발사 횟수
    [SerializeField] private float bulletSpeed = 24f; // 총알 속도

    [SerializeField] private Animator turretAnimator;     // 터렛에 붙은 Animator
    [SerializeField] private string shootParam = "Shoot"; // 애니메이터 파라미터명(Trigger)

    private Transform target; // 타겟
    private float nextFireTime; // 다음 발사 시간

    private void Start()
    {
        if (detector == null) detector = GetComponentInChildren<TurretDetector>();
    }

    private void OnEnable()
    {
        if (detector != null)
            detector.onDetected += SetTarget; // onDetected 이벤트에 SetTarget 함수를 구독
    }

    private void OnDisable()
    {
        if (detector != null)
            detector.onDetected -= SetTarget; // 스크립트가 비활성화될 때 이벤트 구독 해제.
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 aimPos = target.position;
        Vector3 from = (yawPivot ? yawPivot.position : transform.position); // 기준점
        Vector3 dir = (aimPos - from); // 거리

        Quaternion targetRot = Quaternion.LookRotation(dir.normalized, Vector3.up); // 회전값 생성

        if (yawPivot != null) // y축 회전 적용
        {
            Quaternion yawOnly = Quaternion.Euler(0f, targetRot.eulerAngles.y, 0f);
            yawPivot.rotation = Quaternion.RotateTowards(yawPivot.rotation, yawOnly, turnSpeedDeg * Time.deltaTime);
        }


        if (bulletSpawn == null) return;

        // 발사 가능시 발사 
        if (Time.time >= nextFireTime)
        {
            TryFire();
            nextFireTime = Time.time + (1f / Mathf.Max(0.01f, fireRate));
        }
    }

    private void TryFire()
    {
        if (turretAnimator != null && !string.IsNullOrEmpty(shootParam))
        {
            turretAnimator.SetTrigger(shootParam);
        }

        Shoot();
    }
    private void Shoot()
    {
        //오브젝트 풀이나 발사 위치없으면 중단
        if (bulletPool == null || bulletSpawn == null) return;

        var go = bulletPool.Get();
        if (go == null) return;

        var bullet = go.GetComponent<Bullet>();

        if (bullet == null)
        {
            Debug.LogError("프리팹에 Bullet 컴포넌트가 없습니다.", this);
            return;
        }
        //총알 발사
        bullet.Fire(bulletSpawn.position, bulletSpawn.forward, bulletSpeed, ignored: transform);
    }
    //타겟 설정
    public void SetTarget(Transform t)
    {
        target = t;
    }
    //충돌시 작동 중지
    void OnCollisionEnter(Collision collision)
    {
        target = null;
        nextFireTime = float.MaxValue;

        if (turretAnimator != null && !string.IsNullOrEmpty(shootParam))
        {
            turretAnimator.ResetTrigger(shootParam);
        }
    }
}
