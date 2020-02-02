using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetingBackground : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundVerticalLength;

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D> ();
        groundVerticalLength = groundCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -groundVerticalLength) {
            RepositionBackground ();
        }
    }


    private void RepositionBackground() {
        Vector2 groundOffset = new Vector2 (0, groundVerticalLength * 2f);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
