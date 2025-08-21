using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    NONE = -1,
    HEALTH = 0,
    STAMINA
}

[CreateAssetMenu(fileName = "Condition", menuName = "ScriptableObject/Conditions")]
public class Condition : ScriptableObject
{
    [Header("상태 정보")]
    public ConditionType conditionType;
    public float curValue { get; protected set; }
    [SerializeField] float maxValue;
    [SerializeField] float minValue;

    private void Awake()
    {
        curValue = maxValue;
    }

    //Add value to Condition
    public void AddCondition(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    //Subtract value to Condition
    //If Condition is minvalue, return true; -> For isDead ect
    //If Condition is not minvalue, return false;
    public bool SubtractCondition(float amount)
    {
        curValue = Mathf.Max(curValue - amount, minValue);
        return curValue == minValue;
    }
}
