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
        points.Remove(removeThisPoint);
        if (points.Count == 1)
        {
            speed = returnToSingleSpeed;
        }
        cameraSizeChangeRatio = 0f;
        positionFollowSmoothTime = originalPositionFollowSmoothTime;
    }

    private void Update()
    {
        maxDistance = Vector3.zero;
        minDistance = Vector3.zero;
        newPosition = Vector3.zero;
        foreach (Transform point in points)
        {
            newPosition += point.position;
            maxDistance = CalculateMax(point.position, maxDistance);
            minDistance = CalculateMin(point.position, minDistance);
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
            if (heightDifference < verticalBufferHeight)
            {
                if (heightDifference < 0)
                {
                    heightDifference = 0f;
                }
                cameraSizeChangeRatio = 1 - heightDifference / verticalBufferHeight;
                if (cameraHeight < (distanceY + verticalBufferHeight))
                {
                    cameraHeight = distanceY + verticalBufferHeight;
                }
            }
            float widthDifference = cameraWidth - (distanceX + horizontalBufferWidth);
            if (widthDifference < horizontalBufferWidth)
            {
                if (widthDifference < 0)
                {
                    widthDifference = 0f;
                }
                cameraSizeChangeRatio = 1 - widthDifference / horizontalBufferWidth;
                if (cameraWidth < (distanceX + horizontalBufferWidth))
                {
                    cameraWidth = distanceX + horizontalBufferWidth;
                    cameraHeight = cameraWidth / aspectRatio;
                }
            }

            cameraHeight /= 2;
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
