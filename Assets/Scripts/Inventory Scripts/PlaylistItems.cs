using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Item", menuName = "create new audio Item")]
public class PlaylistItems : ScriptableObject
{
    public string audioId;
    public string displayName;
    [TextArea(15, 20)]
    public string Atranscript;
    public string audioLength;
    public Sprite icon;
    public AudioClip audio;
    public bool isNew = false;
}
