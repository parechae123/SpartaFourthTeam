using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugModule : MonoBehaviour
{
    [SerializeField] bool ActiveDebug = false;
    [SerializeField] GameObject testobject;
    DebugManager debugManager;
    private void Awake()
    {
#if UNITY_EDITOR
        if (ActiveDebug)
        {
            DontDestroyOnLoad(gameObject);
            debugManager = new DebugManager(ActiveDebug);
        }
        else
            Destroy(gameObject);
#else
        Destroy(gameObject);
#endif
    }


    public void OnDebugActionOne(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DoActionOne();
        }
    }
    public void OnDebugActionTwo(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DoActionTwo();
        }
    }

    /* Implement Test Codes Here */
    private void DoActionOne()
    {
        Player player = testobject.GetComponent<Player>();
        player.OnDamageAppllied(3);
    }

    private void DoActionTwo()
    {
        Player player = testobject.GetComponent<Player>();
        player.OnHealApplied(4);
    }


}
