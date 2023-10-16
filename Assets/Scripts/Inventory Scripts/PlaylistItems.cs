using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Item", menuName = "create new audio Item")]
public class PlaylistItems : ScriptableObject
{
    public string audioId;
    public string displayName;
    public string audioLength;
    public Sprite icon;
    public AudioClip audio;
}
