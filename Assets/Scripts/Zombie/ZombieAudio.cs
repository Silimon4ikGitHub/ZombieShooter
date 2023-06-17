using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    [SerializeField] private AudioClip zombieHitClip;
    private AudioSource myAudioSource;
    
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        myAudioSource.clip = zombieHitClip;
        myAudioSource.Play();
    }
}
