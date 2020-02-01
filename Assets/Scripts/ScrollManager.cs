using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrollManager : MonoBehaviour
{
    public Camera camera; 
    public int threshold = 200;
    public Grid grid;
    private Vector3 initialPos;
    public MapGenerator mapGenerator;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = grid.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bottom = camera.ScreenToWorldPoint(Vector2.zero);
        Vector3 cell = grid.WorldToCell(bottom);
        int row = Mathf.RoundToInt(cell.y);
        if (row == threshold) {
            grid.transform.position = initialPos;
            mapGenerator.generatNextMap(threshold);
        }
    }
}
