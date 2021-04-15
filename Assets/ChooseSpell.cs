using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSpell : MonoBehaviour
{
    private FireBoltSpawn spawn;
    private SpeedManipulator buffOrDebuff;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponentInChildren<FireBoltSpawn>();
        buffOrDebuff = GetComponent<SpeedManipulator>();
    }

    //elements has a key for each element type implemented and the value associated
    //is equal to the number of times the player selected that element for this spell
    public void ElementsToSpell(Dictionary<string, int> elements, string primary)
    {
        switch (primary)
        {
            case "Arcane":
                if (elements["Fire"] == 0 && elements["Arcane"] == 0)
                    buffOrDebuff.ActivateBuff(.5f, 8f);
                else if (elements["Fire"] == 0 && elements["Arcane"] >= 1 && elements["Arcane"] < 3)
                    buffOrDebuff.ActivateBuff(1.5f, 5f);
                else if (elements["Fire"] == 0 && elements["Arcane"] == 3)
                    buffOrDebuff.ActivateBuff(2.0f, 5f);
                else if (elements["Fire"] == 1)
                    spawn.LaunchSpell(FireBoltSpawn.SpellLevel.minor);
                else if (elements["Fire"] == 2)
                    spawn.LaunchSpell(FireBoltSpawn.SpellLevel.normal);
                else if (elements["Fire"] >= 3)
                    spawn.LaunchSpell(FireBoltSpawn.SpellLevel.major);
                break;

            case "Fire":
                if (elements["Fire"] == 0)
                    spawn.LaunchSpell(FireBoltSpawn.SpellLevel.minor);
                if (elements["Fire"] == 1)
                    spawn.LaunchSpell(FireBoltSpawn.SpellLevel.normal);
                if (elements["Fire"] >= 2)
                    spawn.LaunchSpell(FireBoltSpawn.SpellLevel.major);
                break;
        }
    }
}
