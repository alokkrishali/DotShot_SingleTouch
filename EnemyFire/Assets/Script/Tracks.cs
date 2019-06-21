using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour {

    Transform thisTransform;
    Vector2 trackPos;
    [SerializeField]
    float speed = 20;


    void Start () {
        thisTransform = transform;
        trackPos = thisTransform.position;
        trackPos.x = 14;
        thisTransform.position = trackPos;
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        thisTransform.position = new Vector2(Mathf.Lerp(thisTransform.position.x, -14, speed*Time.deltaTime), trackPos.y);
        if (thisTransform.position.x < -12)
        {
            trackPos.x = 14;
            thisTransform.position = trackPos;
        }
    }
}
