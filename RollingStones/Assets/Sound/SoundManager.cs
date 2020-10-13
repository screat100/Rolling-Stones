using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip JumpSound, DoorSound, MainMusic, FallSound, WallSound;
    AudioSource MyAudio;
    public static SoundManager Instance;

    // Start is called before the first frame update

    void Awake()
    {
        if (SoundManager.Instance == null)
            SoundManager.Instance = this;
    }

    void Start()
    {
        MyAudio = GetComponent<AudioSource>();
    }

    public void PlayMainMusic()
    {
        MyAudio.clip = MainMusic;
        MyAudio.loop = true;
        MyAudio.Play();
    }
    public void PlayJumpSound()
    {
        MyAudio.PlayOneShot(JumpSound);
    }
    public void PlayDoorSound()
    {
        MyAudio.PlayOneShot(DoorSound);
    }
    public void PlayFallSound()
    {
        MyAudio.PlayOneShot(FallSound);
    }
    public void PlayWallSound()
    {
        MyAudio.PlayOneShot(WallSound);
    }

    // Update is called once per frame
    void Update()
    {

    }
}