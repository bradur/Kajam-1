using UnityEngine;
using System.Collections.Generic;

public enum Action
{
    None,
    AimUp,
    AimDown,
    ShootArrow
}

[System.Serializable]
public class GameKey : System.Object
{
    public KeyCode key;
    public Action action;
}

public class KeyManager : MonoBehaviour
{


    public static KeyManager main;

    void Awake()
    {
        main = this;
        /*if (GameObject.FindGameObjectsWithTag("KeyManager").Length == 0)
        {
            main = this;
            gameObject.tag = "KeyManager";
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    [SerializeField]
    private List<GameKey> gameKeys = new List<GameKey>();

    public bool GetKeyDown(Action action)
    {
        if (Input.GetKeyDown(GetKeyCode(action)))
        {
            return true;
        }
        return false;
    }

    public bool GetKeyUp(Action action)
    {
        if (Input.GetKeyUp(GetKeyCode(action)))
        {
            return true;
        }
        return false;
    }

    public bool GetKey(Action action)
    {
        if (Input.GetKey(GetKeyCode(action)))
        {
            return true;
        }
        return false;
    }

    public KeyCode GetKeyCode(Action action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                return gameKey.key;
            }
        }
        return KeyCode.None;
    }

    public string GetKeyString(Action action)
    {
        foreach (GameKey gameKey in gameKeys)
        {
            if (gameKey.action == action)
            {
                string keyString = gameKey.key.ToString();
                if (gameKey.key == KeyCode.Return)
                {
                    keyString = "Enter";
                }
                else if (gameKey.key == KeyCode.RightControl)
                {
                    keyString = "Right Ctrl";
                }
                return keyString;
            }
        }
        return "";
    }
}
