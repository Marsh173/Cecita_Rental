using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "create new item")]
public class playlistItems : ScriptableObject
{
    public string audioId;
    public string displayName;
    public Sprite icon;
    public AudioClip audio;
}
