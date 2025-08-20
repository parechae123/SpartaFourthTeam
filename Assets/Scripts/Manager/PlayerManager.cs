using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("PlayerManager").AddComponent<PlayerManager>();
            return _instance;
        }
    }
    public Player player;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
            Destroy(gameObject);
    }
}
