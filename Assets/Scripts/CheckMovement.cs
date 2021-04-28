using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMovement : MonoBehaviour
{
    private Vector2 _lastPosition;
    public float minDistance = 0.01f;
    public float waitingTime = 0.5f;
    public bool isMoving = false;
    [SerializeField]
    private float timer = 0f;
    private bool waitedEnough = false;

    void Awake()
    {
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(_lastPosition, transform.position) <= minDistance)
        {
            if(!waitedEnough)
                timer += Time.fixedDeltaTime;
        }
        else
        {
            timer = 0f;
            waitedEnough = false;
        }

        _lastPosition = transform.position;
    }

    void Update()
    {
        if (timer >= waitingTime)
        {
            waitedEnough = true;
        }

        if (waitedEnough)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        
    }
}
