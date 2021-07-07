using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Awake is called one the script instance is being loaded
    void Awake()
    {
        SetUpSingelton();
    }

    private void SetUpSingelton()
    {
        // Searching if there is more than one Music Player game object, if there is - destroy it
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
