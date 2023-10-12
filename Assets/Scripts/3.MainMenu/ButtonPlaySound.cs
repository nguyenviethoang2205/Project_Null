using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaySound : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void CLickButtonSound(){
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
