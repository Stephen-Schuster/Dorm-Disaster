using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    Vector3 startingPos;
    Quaternion startingRot;
    public bool scored = false;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        startingRot = transform.rotation;
    }

    public void ResetPos()
    {
        transform.position = startingPos;
        transform.rotation = startingRot;
    }
}
