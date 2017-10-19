// Date   : 19.10.2017 21:22
// Project: Kajam#1
// Author : bradur

using UnityEngine;
using System.Collections;

// from http://answers.unity3d.com/questions/41931/how-to-randomly-change-the-intensity-of-a-point-li.html
[RequireComponent(typeof(Light))]
public class PerlinFlicker : MonoBehaviour
{

    [SerializeField]
    [Range(0f, 10f)]
    private float minIntensity = 0.25f;
    [SerializeField]
    [Range(1f, 20f)]
    private float maxIntensity = 0.5f;

    [SerializeField]
    [Range(0f, 10f)]
    private float minRange = 0.25f;
    [SerializeField]
    [Range(1f, 20f)]
    private float maxRange = 0.5f;

    [SerializeField]
    [Range(-1f, 10f)]
    private float minZHeight = 0.25f;
    [SerializeField]
    [Range(0f, 20f)]
    private float maxZHeight = 0.5f;

    [SerializeField]
    [Range(0f, 3f)]
    private float speed;

    private float random;

    private Light lightObject;

    void Start()
    {
        lightObject = GetComponent<Light>();
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time * speed);
        lightObject.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        float noise2 = Mathf.PerlinNoise(random, Time.time * speed * 2);
        lightObject.range = Mathf.Lerp(minRange, maxRange, noise2);
        float noise3 = Mathf.PerlinNoise(random, Time.time * speed / 2);
        Vector3 newPos = lightObject.transform.position;
        lightObject.transform.position = new Vector3(newPos.x, newPos.y, Mathf.Lerp(minZHeight, maxZHeight, noise3));
    }

}
