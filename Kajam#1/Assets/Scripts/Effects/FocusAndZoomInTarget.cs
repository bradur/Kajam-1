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

    private Transform playerTransform;
    private CenteredAroundPointsCamera capCamera;
    private float originalZ;

    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
        capCamera = mainCamera.GetComponent<CenteredAroundPointsCamera>();
        originalZ = capCamera.transform.position.z;
    }

    void Update () {
        if (active)
        {
            smoothDampRatio -= Time.unscaledDeltaTime * speed;
            ratio += Time.unscaledDeltaTime * speed;
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, minSize, ratio);
            Vector3 newPos = Vector2.SmoothDamp(
                transform.position,
                target.position,
                ref currentVelocity,
                smoothDampRatio,
                1f,
                Time.unscaledDeltaTime
            );
            newPos.z = originalZ;
            transform.position = newPos;
            if (Vector2.Distance(transform.position, target.position) < 0.01f && Mathf.Abs(mainCamera.orthographicSize - minSize) < 0.01f)
            {
                active = false;
            }
        }
    }

    public void Deactivate()
    {
        SoundManager.main.LerpPitchDown(SoundType.BeingPulled);
        SoundManager.main.FadeOutSound(SoundType.BeingPulled);
        Vector3 newPos = playerTransform.position;
        newPos.z = originalZ;
        capCamera.transform.position = newPos;
        capCamera.enabled = true;
        capCamera.RemoveAllPointsButPlayer();
        Time.timeScale = 1f;
        this.target = null;
        ratio = 0f;
        smoothDampRatio = 1f;
        active = false;
    }

    public void Init(Transform target)
    {
        SoundManager.main.LerpPitchUp(SoundType.BeingPulled);
        Time.timeScale = 0f;
        this.target = target;
        ratio = 0f;
        smoothDampRatio = 1f;
        active = true;
        capCamera.enabled = false;
    }
}
