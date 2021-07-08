using UnityEngine;
using MyBox;

[CreateAssetMenu(fileName = "Audio List", menuName = "TinyCacto/Audio List", order = 1)]
public class AudioController : ScriptableObject
{

    [SearchableEnum]
    public TypeFloor materialType;


    public AudioList[] audios;

}

[System.Serializable]
public struct AudioList
{
    public AudioClip clip;

    [SearchableEnum]
    [SerializeField]
    public AudioType type;

    [Range(0, 1)]
    public float volume;
}

public enum AudioType
{
    Footstep,
    Jump,
    Land
}