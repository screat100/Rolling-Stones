using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip JumpSound, DoorSound, MainMusic, FallSound, WallSound, SwampSound, BoosterSound, PropellerSound, JumpPadSound;
    AudioSource MyAudio;
    public static SoundManager Instance;
    public bool isPlaying;

    // Start is called before the first frame update

    void Awake()
    {
        if (SoundManager.Instance == null)
            SoundManager.Instance = this;
    }

    void Start()
    {
        MyAudio = GetComponent<AudioSource>();
        isPlaying = false;
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

    public void PlaySwampSound()
    {
        MyAudio.PlayOneShot(SwampSound);
    }

    public void PlayBoosterSound()
    {
        MyAudio.PlayOneShot(BoosterSound);
    }

    public void PlayPropellerSound()
    {
        MyAudio.PlayOneShot(PropellerSound);
    }

    public void PlayJumpPadSound()
    {
        MyAudio.PlayOneShot(JumpPadSound);
    }


    // Update is called once per frame
    void Update()
    {

    }
}