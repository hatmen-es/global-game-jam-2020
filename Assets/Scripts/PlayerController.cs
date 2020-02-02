using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private bool isDead = false;  
    public int lives = 3; 
    public bool useLives = false;
    private Rigidbody2D playerRb; 
    public MapGenerator mapGenerator;

    public float verticalVelocity = 10f;
    public float horizontalVelocity = 10f;

    private string status = "normal";
    public AudioClip AudioClipFixable;
    AudioSource audioSourceFixable;
    private int obstacleCollisionFrame = 0;
    private int dangerCollisionFrame = -1;
    // Start is called before the first frame update
    void Start()
    {
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        playerRb = GetComponent<Rigidbody2D>();
        audioSourceFixable = gameObject.AddComponent<AudioSource>();
        audioSourceFixable.clip = AudioClipFixable;
    }

    // Update is called once per frame
    void Update()
    {
        //Don't allow control the player if isDead.
        if (isDead == false) {
            if (Input.GetKeyDown(KeyCode.H)) {
                GameController.Instance.setTool(1);
            } else if (Input.GetKeyDown(KeyCode.J)) {
                GameController.Instance.setTool(2);
            } else if (Input.GetKeyDown(KeyCode.K)) {
                GameController.Instance.setTool(3);
            } else if (Input.GetKeyDown(KeyCode.L)) {
                GameController.Instance.setTool(4);
            }
            Vector2 playerInput;
            // Get player input
            playerInput.x = Input.GetAxis("Horizontal") * verticalVelocity;
            playerInput.y = Input.GetAxis("Vertical") * horizontalVelocity;

            //Assign player inputs to rigidBody
            playerRb.velocity = playerInput;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        detectGameoverCollision(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        detectGameoverCollision(collision);
    }

    void detectGameoverCollision(Collision2D collision) {
        int current = Time.frameCount;
        switch(collision.gameObject.tag) {
            case "Obstacle":
                obstacleCollisionFrame = current;
            break;
            case "Danger":
                dangerCollisionFrame = current;
            break;
            default: break;
        }
        detectGameoverCollision();
    }


    void detectGameoverCollision() {
        if (dangerCollisionFrame == obstacleCollisionFrame) {
            GameController.Instance.GameOver();
        }
    }

    void CheckFixableCollision(Collider2D collider) {
        if (collider.gameObject.tag == "Fixable"){ 
            int element = mapGenerator.mapElementInWorldPos(transform.position);
            int selected = GameController.Instance.getSelectedTool();
            if (element == selected) {
                audioSourceFixable.Play();
                GameController.Instance.IncrementTimer();
                mapGenerator.removeFixableAtPosition(transform.position);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        CheckFixableCollision(collider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CheckFixableCollision(collider);
        switch(collider.gameObject.tag) {
            case "Obstacle":
                hitPlayer();
                hitModifier("Slowdown");
            break;
            default: break;
        }
    }

    void killPlayer()
    {
        // Zero out the player's velocity
        playerRb.velocity = Vector2.zero;
        // If the player collides with something set it to dead...
        isDead = true;
        
        //...and tell the game control about it.
        //TODO descomentar una vez tengamos el gamecontroller
        //GameControl.instance.PlayerDied();
    }

    void hitPlayer()
    {
        if(useLives){
            lives -= 1;
            if(lives<=0) killPlayer();
        }
    }

    void hitModifier(string type){
        switch (type) {
            case "Slowdown": 
                if(status!="Slowdown"){
                    verticalVelocity = 1.5f;
                    horizontalVelocity = 1.5f;
                    Invoke("resetPlayerStatus", 4.0f);
                    status = type;
                }    
            break;
        }
    }

    void resetPlayerStatus(){
        verticalVelocity = 10f;
        horizontalVelocity = 10f;
        status="normal";
    }


}
