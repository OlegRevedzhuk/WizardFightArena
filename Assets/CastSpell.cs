using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    public FireBoltSpawn minor;
    public FireBoltSpawn normal;
    public FireBoltSpawn major;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //elements has a key for each element type implemented and the value associated
    //is equal to the number of times the player selected that element for this spell
    public void Cast(Dictionary<string, int> elements, string primary)
    {
        switch (primary)
        {
            case "Arcane":
                if (elements["Fire"] == 0)
                    break;
                if (elements["Fire"] == 1)
                    minor.CastSpell();
                if (elements["Fire"] == 2)
                    normal.CastSpell();
                if (elements["Fire"] >= 3)
                    major.CastSpell();
                break;

            case "Fire":
                if (elements["Fire"] == 0)
                    minor.CastSpell();
                if (elements["Fire"] == 1)
                    normal.CastSpell();
                if (elements["Fire"] >= 2)
                    major.CastSpell();
                break;
        }
    }
}
