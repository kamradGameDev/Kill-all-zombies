using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minY, maxY, minX, maxX, posz;
    public Transform target;
    private void Update()
    {
        this.transform.position = target.position;
        this.transform.position = new Vector3
        (
            Mathf.Clamp(this.transform.position.x, minX, maxX),
            Mathf.Clamp(this.transform.position.y, minY, maxY),
            Mathf.Clamp(this.transform.position.z, posz, posz)
        ); ;
    }
}
