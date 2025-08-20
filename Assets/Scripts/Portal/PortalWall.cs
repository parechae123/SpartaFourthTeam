using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalWall : MonoBehaviour
{
    public void TryPlacePortal(Portal portal, RaycastHit hit)
    {
        portal.gameObject.SetActive(true);
        portal.transform.forward = transform.forward;
        portal.transform.SetParent(transform);
        portal.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        portal.transform.position = hit.point + transform.forward * 0.01f;
        Debug.Log(hit.normal);
    }
}
