using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerList.playerList.Add(this);
    }
}
