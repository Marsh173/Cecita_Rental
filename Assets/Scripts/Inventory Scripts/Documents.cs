using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Document Item", menuName = "create new document Item")]
public class Documents : ScriptableObject
{
    public string displayName;
    [TextArea(5, 10)]
    public string wholeTranscript;
    public List<string> transcript = new List<string>();
    public List<Sprite> images = new List<Sprite>();
    public Sprite image;
}
