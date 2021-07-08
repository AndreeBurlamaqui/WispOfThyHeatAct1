using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class ChangePitchOnAwake : MonoBehaviour
{
    [MinMaxRange(0, 3)]
    public RangedFloat newPitch;

    [MinMaxRange(0,1)]
    public RangedFloat newVolume;

    private AudioSource audioClip;
    private void Awake()
    {
        audioClip = GetComponent<AudioSource>();

        audioClip.pitch = Random.Range(newPitch.Min, newPitch.Max);
        audioClip.volume = Random.Range(newVolume.Min, newVolume.Max);

    }

}
