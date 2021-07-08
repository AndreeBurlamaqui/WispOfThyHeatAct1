using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using MyBox;
using UnityEngine.Audio;

public class AudioManager : AudioSingleton<AudioManager>
{

    [SerializeField]
    private LayerMask layer;
    [SerializeField]
    private List<AudioClip> currentPlaylist = new List<AudioClip>();
    [Space]

    [Foldout("Audio Group Mixers")] [SerializeField] private AudioMixerGroup soundtrackMixer, ambienceMixer, sfxMixer;

    private Audio_Area lastArea;
    private bool activeStay;
    public AudioController lastSFXList;
    public AudioClip currentOst;
    #region SFX
    //By Material
    public void PlaySFX(AudioType type, Vector2 groundPosition, List<AudioController> sfxList, RangedFloat newVolume, RangedFloat newPitch)
    {
        TypeFloor tf = TilemapManager.Instance.GetTypeFloor(groundPosition);
        List<AudioList> randomValues = new List<AudioList>();

        if (tf != TypeFloor.Default)
        {


            foreach (AudioController al in sfxList)
            {
                if (tf == al.materialType)
                {
                    lastSFXList = al;

                    foreach (AudioList at in al.audios)
                    {
                        if (at.type == type)
                        {
                            randomValues.Add(at);
                        }
                    }
                }
            }

        }
        else
        {
            foreach (AudioList lastal in lastSFXList.audios)
            {
                if (lastal.type == type)
                {
                    randomValues.Add(lastal);
                }
            }
        }

        AudioList selectedClip = randomValues[Random.Range(0, randomValues.Count)];


        int sfxID = EazySoundManager.PrepareSound(selectedClip.clip, selectedClip.volume * (Random.Range(newVolume.Min, newVolume.Max)), false, null);
        Audio sfxAudio = EazySoundManager.GetAudio(sfxID);

        sfxAudio.Pitch = Random.Range(newPitch.Min, newPitch.Max);
        //sfxAudio.AudioSource.outputAudioMixerGroup = sfxMixer;
        sfxAudio.Play();
    }

    //Normal SFX
    public void PlaySFX(AudioList audio, RangedFloat newVolume, RangedFloat newPitch)
    {
        int sfxID = EazySoundManager.PrepareSound(audio.clip, audio.volume * (Random.Range(newVolume.Min, newVolume.Max)), false, null);
        Audio sfxAudio = EazySoundManager.GetAudio(sfxID);

        sfxAudio.Pitch = Random.Range(newPitch.Min, newPitch.Max);
        sfxAudio.AudioSource.outputAudioMixerGroup = sfxMixer;
        sfxAudio.Play();

    }

    #endregion

    #region Soundtrack and Ambiences
    public void AddAmbiencePlaylist(AudioClip ac, float volume, Audio_Area newArea)
    {
        if(lastArea == null)
            lastArea = newArea;

        if (!IsThisAmbiencePlaying(ac))
        {
            EazySoundManager.PlaySound(ac, volume, true, null);
            EazySoundManager.GetSoundAudio(ac).AudioSource.outputAudioMixerGroup = ambienceMixer;

            currentPlaylist.Add(ac);
        }
    }

    public bool IsThisAmbiencePlaying(AudioClip ac)
    {
        bool isAmbienceAlreadyPlaying = false;

        for (int x = 0; x < currentPlaylist.Count; x++)
        {
            if (currentPlaylist[x] == ac)
            {
                isAmbienceAlreadyPlaying = true;
            }

        }

        return isAmbienceAlreadyPlaying;
    }

    public void RemoveAmbiencePlaylist(Audio_Area newArea)
    {
        if (lastArea != null)
            lastArea = newArea;

        List<AudioClip> indexToClear = new List<AudioClip>();

        foreach(AudioClip ac1 in currentPlaylist)
        {
            if (!lastArea.ambiences.Contains(ac1))
            {
                indexToClear.Add(ac1);
            }
        }

        foreach(AudioClip ac2 in lastArea.ambiences)
        {
            if (!currentPlaylist.Contains(ac2))
            {
                indexToClear.Add(ac2);
            }
        }

        foreach(AudioClip ac3 in indexToClear)
        {
            int x = currentPlaylist.FindIndex(e => e.Equals(ac3));

            EazySoundManager.GetSoundAudio(currentPlaylist[x]).Stop();
            currentPlaylist.RemoveAt(x);

        }

        IsStayActive = false;
    }


    public void PrepareSoundtrackMixer(AudioClip OST)
    {
        EazySoundManager.GetMusicAudio(OST).AudioSource.outputAudioMixerGroup = soundtrackMixer;
    }

    public bool IsStayActive
    {
        get => activeStay;
        set => activeStay = value;
    }

    public AudioClip CurrentOst
    {
        get => currentOst;
        set => currentOst = value;
    }

    #endregion
}
