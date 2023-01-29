using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource, walkingSource, doorSource;

    [SerializeField] Slider musicSlider, sfxSlider;

    private void Update()
    {
        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;
        walkingSource.volume = sfxSlider.value;
        doorSource.volume = sfxSlider.value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSlider.value = musicSource.volume;
            sfxSlider.value = sfxSource.volume;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("DungeonEcho");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, s => s.name == name);

        if (sound == null)
        {
            Debug.Log("[AUDIO_MANAGER] Sound not found" + name);
        }
        else
        {

            switch (name)
            {
                case "DungeonEcho":
                    musicSource.volume = 0.4f;
                    break;
                default:
                    musicSource.volume = 0.8f;
                    break;
            }

            musicSource.clip = sound.audioClip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, s => s.name == name);

        if (sound == null)
        {
            Debug.Log("[AUDIO_MANAGER] Sound not found" + name);
        }
        else
        {
            AudioSource source;

            switch (sound.sourceId)
            {
                case 0:
                    source = walkingSource;
                    break;
                case 1:
                    source = doorSource;
                    break;
                default:
                    source = sfxSource;
                    break;
            }


            switch (name)
            {
                case "PlayerWalking":
                    source.volume = 0.1f;
                    break;
                default:
                    source.volume = 0.8f;
                    break;
            }

            source.PlayOneShot(sound.audioClip);
        }
    }
}
