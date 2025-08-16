using System;
using UnityEngine;

public class TurretDetector : MonoBehaviour, IDetectAction
{
    [Header("Detection")]
    [SerializeField] private Transform sightOrigin;     // 레이 시작 위치
    [SerializeField] private float detectRadius = 12f; // 감지 반경
    [SerializeField] private float checkInterval = 0.15f; // 탐지 주기
    [SerializeField] private LayerMask targetMask;      // 탐지 대상 레이어
    [SerializeField] private LayerMask obstacleMask;    // 벽/지형
    [SerializeField] private bool requireOfSight = true; // 시야에 가려져있을경우

    public event Action<Transform> onDetected; // 탐지시 해당 타겟의 transform 전달

    private float nextCheckTime; // 다음 탐지 실행 시간

    //기본 초기화
    private void Awake()
    {
        sightOrigin = transform;
        targetMask = LayerMask.GetMask("Player");
        obstacleMask = LayerMask.GetMask("Default");
    }

    private void Update()
    {
        // 플레이어(적) 탐지
        if (Time.time < nextCheckTime) return;
        nextCheckTime = Time.time + checkInterval;

        OnDetect();
    }

    public void OnDetect()
    {
        var origin = sightOrigin ? sightOrigin.position : transform.position;
        var hits = Physics.OverlapSphere(origin, detectRadius, targetMask);

        Transform target = null; //가장 가까운 물체 위치
        float bestSqr = float.MaxValue;

        foreach (var hit in hits)
        {
            //물체와 나의 거리 계산 (제곱근연산 안하려고 sqrMagnitude사용)
            var t = hit.transform;
            float doubleDistance = (t.position - origin).sqrMagnitude;

            if (requireOfSight)
            {
                var dir = (t.position - origin).normalized;
                float dist = Mathf.Sqrt(doubleDistance);

                // 장애물에 가려져 있으면 패스
                if (Physics.Raycast(origin, dir, dist, obstacleMask))
                    continue; 
            }

            if (doubleDistance < bestSqr) // 가장 가까운 타겟 갱신
            {
                bestSqr = doubleDistance;
                target = t;
            }
        }

        if (target != null) //타겟이 존재할경우 onDetected 이벤트 전달
        {       
            onDetected?.Invoke(target);
        }
        else
        {
            onDetected?.Invoke(null);
        }
    }

}
