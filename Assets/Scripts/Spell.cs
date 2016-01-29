using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu]
[System.Serializable]
public class Spell : ScriptableObject{

    public List<Rune> Runes;
    public string SpellName;


}
