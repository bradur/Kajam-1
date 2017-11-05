// Date   : 18.10.2017 23:19
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CenteredAroundPointsCamera : MonoBehaviour
{

    [SerializeField]
    private List<Transform> points;

    [SerializeField]
    private bool followX = true;
    [SerializeField]
    private bool followY = true;

    [SerializeField]
    [Range(0, 1f)]
    private float speed = 0.5f;

    [SerializeField]
    [Range(0, 1f)]
    private float returnToSingleSpeed = 0.05f;

    [SerializeField]
    [Range(0, 1f)]
    private float changeBackSpeed;

    private float aspectRatio;

    private Vector3 startingPosition;

    private Camera thisCamera;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float horizontalBufferWidth = 0.2f;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float verticalBufferHeight = 0.2f;

    [SerializeField]
    [Range(0f, 5f)]
    private float positionFollowSmoothTime = 0.5f;

    private Vector3 newPosition;
    private Vector3 maxDistance;
    private Vector3 minDistance;
    private Vector3 currentVelocity;
    private float cameraSizeChangeRatio = 0f;
    private float cachedSpeed;
    private float originalOrthographicSize;
    private float originalPositionFollowSmoothTime;

    private void Start()
    {
        startingPosition = transform.position;
        thisCamera = GetComponent<Camera>();
        speed = 1 / speed;
        cachedSpeed = speed;
        originalOrthographicSize = thisCamera.orthographicSize;
        aspectRatio = Screen.width / Screen.height;
        originalPositionFollowSmoothTime = positionFollowSmoothTime;
    }

    public void AddPoint(Transform newPoint)
    {
        points.Add(newPoint);
        if (points.Count == 2)
        {
            speed = cachedSpeed;
        }
        cameraSizeChangeRatio = 0f;
        positionFollowSmoothTime = originalPositionFollowSmoothTime;
    }

    public void RemovePoint(Transform removeThisPoint)
    {
        if (points.Remove(removeThisPoint))
        {
            if (points.Count == 1)
            {
                speed = returnToSingleSpeed;
            }
            cameraSizeChangeRatio = 0f;
            positionFollowSmoothTime = originalPositionFollowSmoothTime;
        }
    }

    private void Update()
    {
        maxDistance = points[0].position;
        minDistance = points[0].position;
        newPosition = Vector3.zero;
        foreach (Transform point in points)
        {
            newPosition += point.position;
            Debug.Log(point.position + " => " +minDistance.x + ", " + maxDistance.x);
            maxDistance = CalculateMax(point.position, maxDistance);
            minDistance = CalculateMin(point.position, minDistance);
            Debug.Log(point.position + " => " + minDistance.x + ", " + maxDistance.x);

        }
        float distanceX = Mathf.Abs(maxDistance.x - minDistance.x);
        float distanceY = Mathf.Abs(maxDistance.y - minDistance.y);
        
        float cameraHeight = thisCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * aspectRatio;
        if (points.Count == 1)
        {
            cameraHeight = originalOrthographicSize;
            cameraSizeChangeRatio += Time.deltaTime * speed;
        }
        else
        {
            float heightDifference = cameraHeight - (distanceY + verticalBufferHeight);
            float widthDifference = cameraWidth - (distanceX + horizontalBufferWidth);

            //Debug.Log(distanceX);
            if (heightDifference >= verticalBufferHeight && widthDifference >= horizontalBufferWidth)
            {
                /*Debug.Log(cameraHeight + ">" + (distanceY + verticalBufferHeight));
                Debug.Log(heightDifference + "<" + verticalBufferHeight);*/
                float bufferedVertical = originalOrthographicSize + verticalBufferHeight;
                float bufferedHoriziontal = bufferedVertical * aspectRatio;
                //Debug.Log(cameraHeight + " > " + bufferedVertical + " || " + (cameraWidth / aspectRatio) +" > "+ bufferedHoriziontal);
                
                if (cameraHeight > bufferedVertical || (cameraWidth / aspectRatio) > bufferedHoriziontal)
                {
                    cameraHeight = originalOrthographicSize;
                    cameraSizeChangeRatio += Time.deltaTime * changeBackSpeed;
                }

                //cameraSizeChangeRatio += Time.deltaTime * speed;
                //cameraSizeChangeRatio += Time.deltaTime * speed;
                //cameraSizeChangeRatio = 1 - heightDifference / originalOrthographicSize;
            }
            else
            {
                if (heightDifference < verticalBufferHeight)
                {
                    cameraSizeChangeRatio = 1 - heightDifference / verticalBufferHeight;
                    if (cameraHeight < (distanceY + verticalBufferHeight))
                    {
                        cameraHeight = distanceY + verticalBufferHeight;
                    }
                }
                
                if (widthDifference < horizontalBufferWidth)
                {
                    cameraSizeChangeRatio = 1 - widthDifference / horizontalBufferWidth;
                    if (cameraWidth < (distanceX + horizontalBufferWidth))
                    {
                        cameraWidth = distanceX + horizontalBufferWidth;
                        cameraHeight = cameraWidth / aspectRatio;
                    }
                }
                cameraHeight /= 2;
            }
        }
        positionFollowSmoothTime = originalPositionFollowSmoothTime - (originalPositionFollowSmoothTime * cameraSizeChangeRatio);
        thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, cameraHeight, cameraSizeChangeRatio);
        newPosition /= points.Count;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            ClampCameraPosition(newPosition),
            ref currentVelocity,
            positionFollowSmoothTime
        );
    }

    private Vector3 CalculateMax(Vector3 position, Vector3 max)
    {
        if (position.x > max.x)
        {
            max.x = position.x;
        }
        if (position.y > max.y)
        {
            max.y = position.y;
        }
        return max;
    }

    private Vector3 CalculateMin(Vector3 position, Vector3 min)
    {
        if (position.x < min.x)
        {
            min.x = position.x;
        }
        if (position.y < min.y)
        {
            min.y = position.y;
        }
        return min;
    }

    private Vector3 ClampCameraPosition(Vector3 newPosition)
    {
        if (!followX)
        {
            newPosition.x = startingPosition.x;
        }
        if (!followY)
        {
            newPosition.y = startingPosition.y;
        }
        newPosition.z = startingPosition.z;
        return newPosition;
    }
}
