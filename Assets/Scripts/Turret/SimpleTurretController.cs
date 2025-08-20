using UnityEngine;

public class SimpleTurretController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bulletSpawn;   // 총알 스폰 위치(총구)
    [SerializeField] private ObjectPool bulletPool;   // 오브젝트 풀

    [Header("Fire")]
    [SerializeField] private float fireRate = 5f;     // 초당 발사 횟수
    [SerializeField] private float bulletSpeed = 24f; // 총알 속도

    [SerializeField] private Animator turretAnimator;     
    [SerializeField] private string shootParam = "Shoot"; 

    private float nextFireTime; 
    private bool stopped;

    private void OnEnable()
    {
        stopped = false;
        nextFireTime = Time.time + (1f / Mathf.Max(0.01f, fireRate));
    }

    private void Update()
    {
        if (stopped || bulletSpawn == null || bulletPool == null) return;

        if (Time.time >= nextFireTime)
        {
            if (turretAnimator && !string.IsNullOrEmpty(shootParam))
                turretAnimator.SetTrigger(shootParam);

            ShootForward();
            nextFireTime = Time.time + (1f / Mathf.Max(0.01f, fireRate));
        }
    }

    private void ShootForward()
    {
        var go = bulletPool.Get();
        if (!go) return;

        var bullet = go.GetComponent<Bullet>();
        if (!bullet)
        {
            Debug.LogError("프리팹에 Bullet 컴포넌트가 없습니다.", this);
            return;
        }

        // 항상 총구 forward 방향으로 발사
        bullet.Fire(bulletSpawn.position, bulletSpawn.forward, bulletSpeed, ignored: transform);
    }

    // 충돌 시 정지(원하면 유지)
    private void OnCollisionEnter(Collision collision)
    {
        stopped = true;
        if (turretAnimator && !string.IsNullOrEmpty(shootParam))
            turretAnimator.ResetTrigger(shootParam);
    }
}
