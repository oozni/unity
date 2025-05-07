using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public Transform target;

    protected float offsetX;
    protected float offsetY;
    protected float offsetZ;

    private float cameraSpeed = 10.0f;
    Vector3 targeetPos;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
        offsetZ = transform.position.z - target.position.z;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //if (target == null) return;

        //Vector3 pos = transform.position;
        //pos.x = target.position.x + offsetX;
        //pos.y = target.position.y + offsetY;
        //transform.position = pos;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        targeetPos = new Vector3
            (
                target.transform.position.x + offsetX,
                target.transform.position.y + offsetY,
                target.transform.position.z + offsetZ
            );

        transform.position = Vector3.Lerp(transform.position, targeetPos, Time.deltaTime * cameraSpeed);
    }
}
