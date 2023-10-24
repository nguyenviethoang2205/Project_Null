using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    public GameObject tPlayer;
    public Transform tFollowTarget;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tPlayer == null)
       {
        tPlayer = GameObject.FindWithTag("Player");
        if(tPlayer != null)
        {
            tFollowTarget = tPlayer.transform;
            vcam.LookAt = tFollowTarget;
            vcam.Follow = tFollowTarget;
        }
       }
    }
}
