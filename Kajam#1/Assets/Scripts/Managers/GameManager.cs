// Date   : 12.11.2017 22:58
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
        if(KeyManager.main.GetKeyDown(Action.Quit))
        {
            Application.Quit();
        }
    }
}
