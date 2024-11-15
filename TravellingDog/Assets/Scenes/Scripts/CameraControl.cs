﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject target;
    public float followAhead;
    private Vector3 targetPosition;
    public float smoothtime;
    public bool followTarget;
    void Start()
    {
        followTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(followTarget)
        {
            targetPosition = new Vector3(target.transform.position.x,transform.position.y, transform.position.z);

            if(target.transform.localScale.x > 0f )
            {
               targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
            }
            else{
               targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
             }
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothtime * Time.deltaTime);
        }
    }
}
