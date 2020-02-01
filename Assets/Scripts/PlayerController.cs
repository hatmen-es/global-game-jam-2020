using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead = false;  
    public int lives = 3; 
    public bool useLives = true;
    private Rigidbody2D playerRb; 

    public float verticalVelocity = 10f;
    public float horizontalVelocity = 10f;

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
            break;

            case "Fixeable":
            //TODO descomentar una vez tengamos el gamecontroller
            //GameControl.instance.fix(collision.gameObject.tag);
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
}
