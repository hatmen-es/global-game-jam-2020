using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    public float timer = 0.0f;
    public AudioClip AudioClipBird;
    AudioSource audioSourceBird;
    void Start()
    {
        audioSourceBird = gameObject.AddComponent<AudioSource>();
        audioSourceBird.clip = AudioClipBird;
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

     void OnTriggerEnter2D (Collider2D other)
     {
         if (other.gameObject.tag == "Player") 
         {
             audioSourceBird.Play();
         }
     }
}
