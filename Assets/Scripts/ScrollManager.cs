using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrollManager : MonoBehaviour
{
    public Camera camera; 
    public int threshold = 200;
    public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bottom = camera.ScreenToWorldPoint(Vector2.zero);
        Vector3 cell = grid.WorldToCell(bottom);
        int row = Mathf.RoundToInt(cell.y);
        Debug.Log(row);
    }
}
