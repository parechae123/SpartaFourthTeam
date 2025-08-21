using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isUiOpen = false;
    //Default player components
    IMoveable playerController;
    

    public void Awake()
    {
        PlayerManager.Instance.player = this;
        playerController = GetComponent<IMoveable>();
    }
}
