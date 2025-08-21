using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPortal;
    public Portal GetOtherPortal { get { return otherPortal; } }
    public Portal SetOtherPortal { set { otherPortal = value; } }
    public bool isPortalActive;
    public PortalCollider portalCollider;

    public const float PORTALMINDISTANCE = 5.0f;

    private void Awake()
    {
        isPortalActive = false;
    }

    private void OnEnable()
    {
        isPortalActive = true;
    }

    private void OnDisable()
    {
        isPortalActive = false;
    }

    public void RefreshPortal()
    {
        isPortalActive = true;
        if (GetOtherPortal.isPortalActive)
        {
            Collider othercollider = otherPortal.portalCollider.GetComponent<Collider>();
            othercollider.enabled = false;
            othercollider.enabled = true;
        }
        Collider thiscollider = portalCollider.GetComponent<Collider>();
        thiscollider.enabled = false;
        thiscollider.enabled = true;

    }
}
