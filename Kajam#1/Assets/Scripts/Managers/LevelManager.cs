// Date   : 11.11.2017 16:34
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {


    [SerializeField]
    private Transform LevelContainer;

    [SerializeField]
    private List<Level> levels;

    public static LevelManager main;

    private void Awake()
    {
        main = this;
    }


    [SerializeField]
    private int currentLevel = 0;

    public void LoadNextLevel() {
        if (currentLevel > levels.Count - 2)
        {
            Debug.Log("The end!");
        } else
        {
            levels[currentLevel].Deactivate();
            currentLevel += 1;
            levels[currentLevel].gameObject.SetActive(true);
            levels[currentLevel].Init();
        }
    }
}
