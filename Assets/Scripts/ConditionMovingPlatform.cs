using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConditionMovingPlatform : MonoBehaviour, IMoveable
{
    [SerializeField] private float platformMoveSpeed;
    [SerializeField] private float travelDistance; //플랫폼이 한 방향으로 움직일 최대 거리
    [SerializeField] private Vector3[] moveDirection;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 initPos;
    private Rigidbody platformBody;

    [SerializeField]bool isRecall = false;
    private sbyte currPathIndex = 0;

    bool isPathChange = false;
    private sbyte nextPathIndex = 0;

    [SerializeField] private GameObject triggerGOBJ;
    IObjectTrigger trigger;

    public float moveSpeed { get; set; }

    private void Awake()
    {
        platformBody = GetComponent<Rigidbody>();

        initPos = platformBody.position;
        startPosition = Vector3.zero;

        moveSpeed = platformMoveSpeed;
        targetPosition = moveDirection[currPathIndex];

        triggerGOBJ.TryGetComponent<IObjectTrigger>(out trigger);

        trigger.OnValueChanged = SetPatrolPath;
    }
    private void FixedUpdate()
    {
        OnMove(targetPosition * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(Vector3 movement)
    {
        
        if (GetUclideanDistance((startPosition.normalized * travelDistance) + initPos, transform.position) > travelDistance* travelDistance)
        {

            isRecall = !isRecall;

            if (isPathChange && !isRecall)
            {
                currPathIndex = nextPathIndex;
                nextPathIndex = -1;//비정상처리 확인용
                isPathChange = false;
            }
            startPosition = isRecall? moveDirection[currPathIndex] : Vector3.zero;
            targetPosition = isRecall? -moveDirection[currPathIndex] : moveDirection[currPathIndex];
            platformBody.MovePosition((startPosition.normalized*travelDistance) + initPos);
            
            Debug.Log(transform.position);
            return;
        }
        platformBody.MovePosition(transform.position + movement);
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody rb = other.rigidbody;
        if (rb != null)
        {
            other.transform.SetParent(transform, true);
        }
    }


    private void OnCollisionExit(Collision other)
    {
        Rigidbody rb = other.rigidbody;
        if (rb != null)
        {
            other.transform.SetParent(null, true);
        }
    }

    private void SetPatrolPath(bool isActivate)
    {
        nextPathIndex = isActivate ? (sbyte)0 : (sbyte)1;
        isPathChange = true;
    }

    private float GetUclideanDistance(Vector3 a,Vector3 b)
    {
        a.x -= b.x;
        a.x *= a.x;

        a.y -= b.y;
        a.y *= a.y;

        a.z -= b.z;
        a.z *= a.z;
        return a.x + a.y + a.z;
    }
}