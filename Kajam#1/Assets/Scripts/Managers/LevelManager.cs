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
    private int levelToLoad = 0;

    private void Start()
    {
        LoadNextLevel();
    }

    public void SetLevelStart()
    {
        levelToLoad = 1;
    }

    public void LoadNextLevel() {
        if (levelToLoad > levels.Count - 1)
        {
            Debug.Log("The end!");
        } else
        {
            if (levelToLoad != 0)
            {
                levels[levelToLoad - 1].Deactivate();
            } else if (levelToLoad == 1)
            {
                levels[levels.Count - 1].Deactivate();
            }
            levels[levelToLoad].gameObject.SetActive(true);
            levels[levelToLoad].Init();
            levelToLoad += 1;
        }
    }
}
