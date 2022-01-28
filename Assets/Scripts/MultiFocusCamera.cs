using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiFocusCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset; 

    //bound range
    public float minZoom = 54f;
    public float maxZoom = 16f;
    public float zoomLimiter = 60f;

    //smoothing the camera
    private Vector3 velocity;
    public float smoothTime = 0.5f;
    Camera cam;
    // Start is called before the first frame update
    Bounds bounds;
    void Start()
    {
        cam = GetComponent<Camera>(); 
    }
    void LateUpdate()
    {
        if(targets.Count == 0)
        {
            return;
        }
        Move();     
    }
    void zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, getGreatestDistance()/50);
        cam.fieldOfView = newZoom;
    }
     float getGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
     void Move()
    {
        //sets the position of camera at the center of the targets
        Vector3 centerPoint = GetCenterPoint(); 
        //camera able follow around the player 
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        
        if(targets[0] != null)
        {
            bounds = new Bounds(targets[0].position, Vector3.zero);
        }
        for(int i = 0; i < targets.Count; i++)
        {
            if(targets[i] !=null)
            {
            bounds.Encapsulate(targets[i].position);
            }    
        }
        return bounds.center;
    }

}
