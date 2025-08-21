using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCollider : MonoBehaviour
{
    [HideInInspector] Portal parentPortal;

    private void Awake()
    {
        parentPortal = transform.parent.GetComponent<Portal>();
        if (parentPortal == null)
            throw new System.Exception("Parent doesn't have portal");
    }

    private void OnEnable()
    {
        Collider portalCollider = GetComponent<Collider>();
        parentPortal.isPortalActive = true;
        portalCollider.enabled = false;
        portalCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!parentPortal.GetOtherPortal.isPortalActive || !parentPortal.isPortalActive)
            return;
        if (!collider.TryGetComponent<Rigidbody>(out Rigidbody rigid) || rigid.isKinematic)
            return;
        parentPortal.GetOtherPortal.isPortalActive = false;
        collider.transform.position = parentPortal.GetOtherPortal.transform.position + parentPortal.GetOtherPortal.transform.forward;
        rigid.velocity = parentPortal.GetOtherPortal.transform.forward * rigid.velocity.magnitude;
        collider.transform.eulerAngles = new Vector3(collider.transform.eulerAngles.x, parentPortal.GetOtherPortal.transform.forward.x * 90.0f);
    }

    private void OnTriggerExit(Collider other)
    {
        parentPortal.isPortalActive = true;
    }
}
