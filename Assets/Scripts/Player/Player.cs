using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    //Default player components
    IMoveable playerController;
    
    //Player Info
    [Header("플레이어 상태")]
    [SerializeField] Condition[] conditions;
    [SerializeField] public Inventory playerInventory;
    bool isDead = false;

    public int Health
    {
        get { return healthCondition == null ? -1 : (int)healthCondition.curValue; }
        set { return; }
    }
    private Condition healthCondition;

    public void Awake()
    {
        playerController = GetComponent<IMoveable>();
    }

    public void Start()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            conditions[i] = Instantiate(conditions[i]);
            if (conditions[i].conditionType == ConditionType.HEALTH)
            {
                if (healthCondition != null)
                    throw new System.Exception("Multiple Health Condition in Player");
                healthCondition = conditions[i];
            }
        }
        playerInventory.gameObject.SetActive(false);
    }
    public void OnDamageAppllied(int damage)
    {
        if (isDead)
            return;
        if (healthCondition == null)
            throw new System.Exception("Player doesn't have Health Condition");
        Debug.Log(Health);
        if (healthCondition.SubtractCondition(damage))
            OnDead();
    }

    public void OnHealApplied(int heal)
    {
        if (isDead) return;
        if (healthCondition == null)
            throw new System.Exception("Player doesn't have Health Condition");
        healthCondition.AddCondition(heal);
    }

    public Condition GetConditionByType(ConditionType conditionType)
    {
        IEnumerable<Condition> query = from condition in conditions
                                       where condition.conditionType == conditionType
                                       select condition;
        if (query.Any())
            return query.First();
        return null;
    }

    private void OnDead()
    {
        isDead = true;
        Debug.Log("Player is Dead");
        //죽었을 시 추가 처리
    }
}
