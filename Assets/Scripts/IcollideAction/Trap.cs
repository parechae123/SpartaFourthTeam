using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, ICollideAction
{
    public int damage = 10;  // ���ط� ����

    public Collider objectCollider { get; set; }  

    private void Awake()
    {

        objectCollider = GetComponent<Collider>();
    }

    // �浹 �߻� �� ȣ��Ǵ� �������̽� �޼���
    public void OnCollide(Collider collider)
    {
        // �浹�� ���濡 Idamageable�� �ִ��� üũ
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnDamageAppllied(damage);  // ���� ����
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       OnCollide(collision.collider);  // �浹 �� OnCollide ȣ��
    }

    
}
