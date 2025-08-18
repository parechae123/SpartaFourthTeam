using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closedPosition;

    // 문을 여는 속도
    [SerializeField] private float openSpeed = 1f;

    [SerializeField] private GameObject[] triggerGameObjects;
    private IObjectTrigger[] openTriggers;

    private bool isOpened = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = closedPosition;
        transform.localPosition = closedPosition;
        List<IObjectTrigger> triggerList = new List<IObjectTrigger>();
        for (int i = 0; i < triggerGameObjects.Length; i++)
        {
            if (triggerGameObjects[i].TryGetComponent<IObjectTrigger>(out IObjectTrigger result))
            {
                result.OnValueChanged = SetDoorStatus;
                triggerList.Add(result);
            }
        }
        openTriggers = triggerList.ToArray();
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, openSpeed * Time.deltaTime);
    }

    private void SetDoorStatus(bool isActivate)
    {
        bool openCondition = openTriggers.All(x => x.IsActivated);

        if (openCondition) Open();
        else Close();
    }


    public void Open()
    {
        if (!isOpened)
        {
            isOpened = true;
            targetPosition = openPosition;
        }
    }

    public void Close()
    {
        if (isOpened)
        {
            isOpened = false;
            targetPosition = closedPosition;
        }
    }
}