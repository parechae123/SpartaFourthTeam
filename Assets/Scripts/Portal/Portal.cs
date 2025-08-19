using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPortal;
    public bool isPortalActive;
    [HideInInspector]public Collider portalCollider;

    private void Awake()
    {
        isPortalActive = false;
        portalCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        isPortalActive = true;
    }

    private void OnDisable()
    {
        isPortalActive = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!otherPortal.isPortalActive || !isPortalActive)
            return;
        otherPortal.isPortalActive = false;
        collider.transform.position = otherPortal.transform.position + otherPortal.transform.right;
        collider.transform.rotation = Quaternion.Euler(
            collider.transform.rotation.eulerAngles.x,
            -otherPortal.transform.rotation.eulerAngles .z,
            collider.transform.rotation.eulerAngles.z
            );
    }

    private void OnTriggerExit(Collider other)
    {
        isPortalActive = true;
    }

}
