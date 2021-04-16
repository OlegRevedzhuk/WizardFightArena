using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Etype = ElementContainer.ElementType;
using LevelType = ProjectileSpawn.SpellLevel;
using SplType = ProjectileSpawn.SpellType;

public class ChooseSpell : MonoBehaviour
{
    private ProjectileSpawn spawn;
    private SpeedManipulator buffOrDebuff;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponentInChildren<ProjectileSpawn>();
        buffOrDebuff = GetComponent<SpeedManipulator>();
    }

    //elements has a key for each element type implemented and the value associated
    //is equal to the number of times the player selected that element for this spell
    public void ElementsToSpell(Dictionary<Etype, int> elements, Etype primary)
    {
        switch (primary)
        {
            case Etype.arcane:
                if (elements[Etype.fire] == 0 && elements[Etype.arcane] == 0)
                    buffOrDebuff.ActivateBuff(.5f, 8f);
                else if (elements[Etype.fire] == 0 && elements[Etype.arcane] >= 1 && elements[Etype.arcane] < 3)
                    buffOrDebuff.ActivateBuff(2f, 20f);
                else if (elements[Etype.fire] == 0 && elements[Etype.arcane] == 3)
                    buffOrDebuff.ActivateBuff(3f, 5f);
                else if (elements[Etype.fire] == 1)
                    spawn.LaunchSpell((int) SplType.fireBolt, (int) LevelType.minor);
                else if (elements[Etype.fire] == 2)
                    spawn.LaunchSpell((int) SplType.fireBolt, (int) LevelType.normal);
                else if (elements[Etype.fire] >= 3)
                {
                    spawn.LaunchSpell((int)SplType.magicMissle, (int)LevelType.minor);
                }
                    
                break;

            case Etype.fire:
                if (elements[Etype.fire] == 0)
                    spawn.LaunchSpell((int) SplType.fireBolt, (int) LevelType.minor);
                if (elements[Etype.fire] == 1)
                    spawn.LaunchSpell((int) SplType.fireBolt, (int) LevelType.normal);
                if (elements[Etype.fire] >= 2)
                    spawn.LaunchSpell((int) SplType.fireBolt, (int) LevelType.major);
                break;
        }
    }
}
