// Date   : 10.11.2017 23:00
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class FocusAndZoomInTarget : MonoBehaviour {

    private Transform target;
    private bool active = false;
    private Camera mainCamera;
    [SerializeField]
    [Range(0.01f, 30f)]
    private float speed = 5f;
    private float ratio = 0f;
    private float smoothDampRatio = 1f;
    private Vector2 currentVelocity;

    [SerializeField]
    [Range(0, 1f)]
    private float minSize = 0.5f;

    void Start () {
        mainCamera = Camera.main;
    }

    void Update () {
        if (active)
        {
            smoothDampRatio -= Time.unscaledDeltaTime * speed;
            ratio += Time.unscaledDeltaTime * speed;
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, minSize, ratio);
            transform.position = Vector2.SmoothDamp(
                transform.position,
                target.position,
                ref currentVelocity,
                smoothDampRatio,
                1f,
                Time.unscaledDeltaTime
            );
            if (Vector2.Distance(transform.position, target.position) < 0.01f && Mathf.Abs(mainCamera.orthographicSize - minSize) < 0.01f)
            {
                active = false;
                Debug.Log("reached!");
            }
        }
    }

    public void Init(Transform target)
    {
        Time.timeScale = 0f;
        this.target = target;
        ratio = 0f;
        smoothDampRatio = 1f;
        active = true;
        mainCamera.GetComponent<CenteredAroundPointsCamera>().enabled = false;
    }
}
