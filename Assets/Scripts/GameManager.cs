using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CinemachineVirtualCamera mainCam, rocketManCam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
  

    public void ChangeCam()
    {
        mainCam.enabled = false;
        rocketManCam.enabled = true;
    }
}
