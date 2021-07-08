using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using MyBox;
public class Audio_Player : MonoBehaviour
{

    [MinMaxRange(0, 3)]
    public RangedFloat newPitch;

    [MinMaxRange(0, 1)]
    public RangedFloat newVolume;

    public LayerMask layer;

    [SerializeField]
    public List<AudioController> audioList = new List<AudioController>();

    [Separator]
    [SerializeField] private float groundcheckOffset = 1;

    private void Awake()
    {
        EazySoundManager.IgnoreDuplicateSounds = false;

    }

    public void SFXByMaterial(AudioType type)
    {
        Vector2 groundPosition = new Vector2(transform.position.x, transform.position.y - groundcheckOffset);
        
        if(TilemapManager.Instance.GetTypeFloor(groundPosition) != 0)
            AudioManager.Instance.PlaySFX(type, groundPosition, audioList, newVolume, newPitch);

    }

}
