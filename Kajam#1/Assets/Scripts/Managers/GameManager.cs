// Date   : 12.11.2017 22:58
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    void Start () {
        Cursor.visible = false;
    }

    private bool quitting = false;

    [SerializeField]
    private GameObject quittingMessage;

    void Update () {
        if (KeyManager.main.GetKeyDown(Action.Quit))
        {
            quittingMessage.SetActive(!quittingMessage.activeSelf);
            quitting = !quitting;
            if (quitting)
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
            }
        }
        if(KeyManager.main.GetKeyDown(Action.QuitForReal) && quitting)
        {
            Time.timeScale = 1f;
            Application.Quit();
        }
        if (KeyManager.main.GetKeyDown(Action.Restart) && quitting)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
        if (KeyManager.main.GetKeyDown(Action.MuteMusic))
        {
            SoundManager.main.ToggleMusic();
        }
        if (KeyManager.main.GetKeyDown(Action.MuteSfx))
        {
            SoundManager.main.ToggleSfx();
        }
    }
}
