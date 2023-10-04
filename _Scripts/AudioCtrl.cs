using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public AudioSource background;
    public Transform entrance;
    public AudioClip Normal;
    public AudioClip Furious;
    public AudioClip GameStart;
    private bool hasPlayedFurious = false;
    

    private void Start()
    {
        background.clip = GameStart;
        background.Play();
        
    }
    private void Update()
    {
        StartMusic();
        UpdateMusic();
    }
    private void StartMusic()
    {
        if (background.isPlaying) return;
       if(PlayerCtrl.Instance.transform.position.x > entrance.transform.position.x)
        {
            background.clip = Normal;
            background.Play();
            if(background.isPlaying)
            {
                Debug.Log("Playing");
            }
            

        }

    }

    private void UpdateMusic()
    {

        if (BossCtrl.Instance != null && BossCtrl.Instance.bossHealth.currentHealth <= 1000 && background.isPlaying)
        {
            if (!hasPlayedFurious)
            {
                background.clip = Furious;
                background.Play();
                hasPlayedFurious = true;
            }
        }
        else
        {
            hasPlayedFurious = false;
        }
    }
}

