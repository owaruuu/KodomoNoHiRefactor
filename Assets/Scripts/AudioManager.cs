using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SoundEffectSource, MusicSource;
    public Slider volumeSlider;
    public AudioMixer masterMixer;
    public TextMeshProUGUI volumeNumber;

    [Range(-40,0)]
    public float defaultVolume;

    private void Awake()
    {
        volumeSlider.value = defaultVolume;
        float tempNumber = Mathf.InverseLerp(-40, 0, volumeSlider.value);
        tempNumber = tempNumber * 100;
        volumeNumber.text = tempNumber.ToString();
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    private void Start()
    {
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        masterMixer.SetFloat("masterVol", volumeSlider.value);
        float tempNumber = Mathf.InverseLerp(-40, 0, volumeSlider.value);
        tempNumber = tempNumber * 100;
        volumeNumber.text = Mathf.CeilToInt(tempNumber).ToString();
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        SoundEffectSource.PlayOneShot(clip, volume);
    }

    public void PlaySound(AudioClip clip)
    {
        SoundEffectSource.PlayOneShot(clip);
    }

    public void PlaySounds(AudioClip[] clips, float[] Volumes)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            SoundEffectSource.PlayOneShot(clips[i], Volumes[i]);
        }
    }

    public void ChangeMusic(AudioClip music)
    {
        MusicSource.clip = music;
        MusicSource.Play();
    }

    public void ChangeMusic(AudioClip music, float volume = 1f)
    {
        MusicSource.clip = music;
        MusicSource.volume = volume;
        MusicSource.Play();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }
}
