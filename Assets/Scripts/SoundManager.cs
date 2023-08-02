using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool mute = false;
    [Range(0, 1)]
    public float Volume = 1f;

    public AudioSource SoundEffect;
    public AudioSource Music;

    public SoundType[] Sounds;

    private void Start()
    {
        PlayMusic(SoundEvents.BackgroundMusic);
    }

    public void Mute()
    {
        if (mute)
        {
            mute = false;
            Volume = 1f;
        }
        else
        {
            mute = true;
            Volume = 0f;
        }
    }

    public void PlayMusic(SoundEvents sound)
    {

        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {

            Music.clip = clip;
            Music.Play();
        }
        else
        {
            Debug.LogError("No Clip found for the event");
        }
    }

    public void Play(SoundEvents sound)
    {
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            SoundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No Clip found for the event");
        }
    }

    private AudioClip getSoundClip(SoundEvents sound)
    {
        SoundType Clip = Array.Find(Sounds, i => i.soundType == sound);

        if (Clip != null)
        {
            return Clip.soundClip;
        }
        else
        {
            return null;
        }
    }



}
[Serializable]
public class SoundType
{
    public SoundEvents soundType;
    public AudioClip soundClip;
}

public enum SoundEvents
{
    ButtonClick,
    BackgroundMusic,
    BulletHit,
    BulletShoot,
    EnemyDestroy
}