using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnClick : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public void WhenClick()
    {
        Debug.Log("Test");
        audioSource.PlayOneShot(audioClip);
    }
}
