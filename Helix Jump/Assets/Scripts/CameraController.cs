using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Properties...
    public GameObject ball;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
        offset.y = transform.position.y - ball.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ball.transform.position + new Vector3(0.0f, offset.y, offset.z);
    }
}
