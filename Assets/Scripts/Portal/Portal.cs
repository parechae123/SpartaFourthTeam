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


}
