using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrid : CameraMain
{
    // Start is called before the first frame update
    protected override void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (target == null) return;
        
        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
