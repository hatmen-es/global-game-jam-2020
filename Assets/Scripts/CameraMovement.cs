using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Units per sec
    public float speed = 1;
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (GameController.Instance && GameController.Instance.gameOver)
            return;
        float diff = speed * Time.deltaTime;
        transform.position += new Vector3(0, diff, 0);
    }
}
