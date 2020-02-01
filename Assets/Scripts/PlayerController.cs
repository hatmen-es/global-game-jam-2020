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

    // Start is called before the first frame update
    void Start()
    {
        //Get and store a reference to the Rigidbody2D attached to this GameObject.
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Don't allow control the player if isDead.
        if (isDead == false)
        {
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
        //Should ignore borders
        switch(collision.gameObject.tag) {
            case "Border":
                return;
            break;

            case "Obstacle":
                hitPlayer();
                hitModifier("Slowdown");
            break;

            default: break;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        //Should ignore borders
        switch(collider.gameObject.tag) {
            case "Fixable":
                int element = mapGenerator.mapElementInWorldPos(transform.position);
                Debug.Log(element);
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
                    verticalVelocity = verticalVelocity/3;
                    horizontalVelocity = horizontalVelocity/3;
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
