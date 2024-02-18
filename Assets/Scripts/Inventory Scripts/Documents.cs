using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Document Item", menuName = "create new document Item")]
public class Documents : ScriptableObject
{
    public string displayName;
    [TextArea(5,10)]
    public List<string> transcript = new List<string>();
    public Sprite image;
}
