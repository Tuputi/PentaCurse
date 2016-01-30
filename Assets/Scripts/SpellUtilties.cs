using UnityEngine;
using System.Collections;

public static class SpellUtilties
{
    public static SpellResult GetResult(Spell a, Spell b)
    {
        if(a.Runes.Count == 0 && b.Runes.Count == 0) {
            return SpellResult.Equal;
        }else if(a.Runes.Count == 0) {
            return SpellResult.Winning;
        } else if(b.Runes.Count == 0) {
            return SpellResult.Losing;
        }

        if(a.Runes.Count == 2) {
            if(b.Runes.Count == 2) {
                return SpellResult.Equal;
            }else if(b.Runes.Count == 3) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 4) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 5) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 6) {
                return SpellResult.Losing;
            }
        } if (a.Runes.Count == 3) {
            if (b.Runes.Count == 2) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 3) {
                return SpellResult.Equal;
            } else if (b.Runes.Count == 4) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 5) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 6) {
                return SpellResult.Losing;
            }
        } if (a.Runes.Count == 4) {
            if (b.Runes.Count == 2) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 3) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 4) {
                return SpellResult.Equal;
            } else if (b.Runes.Count == 5) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 6) {
                return SpellResult.Winning;
            }
        } if (a.Runes.Count == 5) {
            if (b.Runes.Count == 2) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 3) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 4) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 5) {
                return SpellResult.Equal;
            } else if (b.Runes.Count == 6) {
                return SpellResult.Winning;
            }
        } if (a.Runes.Count == 6) {
            if (b.Runes.Count == 2) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 3) {
                return SpellResult.Winning;
            } else if (b.Runes.Count == 4) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 5) {
                return SpellResult.Losing;
            } else if (b.Runes.Count == 6) {
                return SpellResult.Equal;
            }
        }

        return SpellResult.Invalid;
    }
}
