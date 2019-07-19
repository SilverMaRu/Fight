using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/FighterSpawInfo")]
public class FighterSpawInfo : ScriptableObject
{
    public string fighterName;
    public string[] skillTypeNames;
}
