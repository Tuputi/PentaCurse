using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu]
[System.Serializable]
public class Spell : ScriptableObject{

    public List<RuneType> Runes;
    public string SpellName;
    public Sprite RuneSymbol;
    public AudioClip SendSound;


}
