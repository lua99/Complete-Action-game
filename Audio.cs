using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource[] soundEffects;
    public static Audio instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void PlaySfx(int soundtoPlay)
    {
        soundEffects[soundtoPlay].Play();
    }




}
