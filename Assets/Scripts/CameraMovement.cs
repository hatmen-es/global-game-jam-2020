using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Units per sec
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (GameController.Instance && GameController.Instance.gameOver)
            return;
        float diff = GameController.Instance.scrollSpeed * Time.deltaTime;
        transform.position += new Vector3(0, diff, 0);
    }
}
