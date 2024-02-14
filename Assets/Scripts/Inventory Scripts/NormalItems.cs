using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Normal Item",menuName = "create new Normal Item")]
public class NormalItems : ScriptableObject
{
    public string itemName;
    public string displayName;
    [TextArea(5,5)]
    public string descriptions;
    public Sprite icon;
}
