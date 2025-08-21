using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, ICollideAction
{
    public int damage = 10;  // 피해량 설정

    public Collider objectCollider { get; set; }  

    private void Awake()
    {

        objectCollider = GetComponent<Collider>();
    }

    // 충돌 발생 시 호출되는 인터페이스 메서드
    public void OnCollide(Collider collider)
    {
        // 충돌한 상대방에 Idamageable이 있는지 체크
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnDamageAppllied(damage);  // 피해 적용
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       OnCollide(collision.collider);  // 충돌 시 OnCollide 호출
    }

    
}
