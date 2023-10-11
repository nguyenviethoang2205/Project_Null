using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void PlayLoopSound(){
        StartCoroutine(PlaySoundContinuously());
    }

    private IEnumerator PlaySoundContinuously()
    {
        while (true) // Lặp vô hạn
        {
            audioSource.PlayOneShot(audioSource.clip);
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }
}

