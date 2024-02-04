using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Document Item", menuName = "create new document Item")]
public class Documents : ScriptableObject
{
    public string docName;
    public string transcript;
    public Sprite image;
}
