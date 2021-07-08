using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using MyBox;

public class Audio_Area : MonoBehaviour
{
    public AudioClip soundtrack;

    public bool hasIntro;

    [ConditionalField(nameof(hasIntro))] public AudioClip intro;

    public List<AudioClip> ambiences = new List<AudioClip>();

    [Range(0,1)]
    public float volumeOST, volumeAmbience;

    public float fadeIn, fadeOut;

    private void Start()
    {
        EazySoundManager.IgnoreDuplicateMusic = false;
    }

    //ADD to Playlist
    public void OnEnterArea()
    {
        AudioClip oldOST = AudioManager.Instance.currentOst;

        if (oldOST != null)
        {            

                if (oldOST != soundtrack)
                {
                    if (!hasIntro)
                    {
                        EazySoundManager.PlayMusic(soundtrack, volumeOST, true, true, fadeIn, fadeOut);
                        AudioManager.Instance.PrepareSoundtrackMixer(soundtrack);
                    }
                    else
                    {
                        StartCoroutine(SoundtrackIntro());
                    }

                AudioManager.Instance.currentOst = soundtrack;

            }

        }
        else
        {
            AudioManager.Instance.currentOst = soundtrack;

            if (!hasIntro)
            {
                EazySoundManager.PlayMusic(soundtrack, volumeOST, true, true, fadeIn, fadeOut);
                AudioManager.Instance.PrepareSoundtrackMixer(soundtrack);
            }
            else
            {
                StartCoroutine(SoundtrackIntro());
            }
        }

        foreach (AudioClip ac in ambiences)
                {

                    AudioManager.Instance.AddAmbiencePlaylist(ac, volumeAmbience, GetComponent<Audio_Area>());

                }
            
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (AudioManager.Instance.IsStayActive)
        {
            if (collision.CompareTag("Player"))
            {
                AudioManager.Instance.RemoveAmbiencePlaylist(GetComponent<Audio_Area>());
            }
        }
    }

    //REMOVE from playlist
    public void OnExitArea()
    {
            AudioManager.Instance.IsStayActive = true;

    }


    IEnumerator SoundtrackIntro()
    {
        EazySoundManager.PlayMusic(intro, volumeOST, false, false, fadeIn, fadeOut);
        AudioManager.Instance.PrepareSoundtrackMixer(intro);

        yield return new WaitForSeconds(intro.length);

        EazySoundManager.PlayMusic(soundtrack, volumeOST, true, true, fadeIn, fadeOut);
        AudioManager.Instance.PrepareSoundtrackMixer(soundtrack);

    }
}
