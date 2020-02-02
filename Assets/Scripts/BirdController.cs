using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    public float timer = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 15.0f){
            Destroy (this.gameObject);
        }
        if (GameController.Instance && GameController.Instance.gameOver) return;
        float diff = speed * Time.deltaTime;
        transform.position += new Vector3(diff, 0, 0);
    }
}
