using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile baseTile;
    public Tile leftTile;
    public Tile rightTile;
    void Start() { 
        int [,] map = Generate(10, 200, 1);
        RenderMap(map);
    }
    public static int[,] Generate(int width, int height, int nElements)
    {
        int[,] map = new int[width, height];
        for (int y = 0; y < height; y++) {
            bool isElementSet = false;
            for (int x = 0; x < width; x++) {
                if (isElementSet) {
                    map[x, y] = 0;
                }
                else {
                    if (Random.Range(0,1) == 0) {
                        map[x, y] = Random.Range(1, nElements);
                        isElementSet = true;
                    }
                }
            }
        }
        return map;
    }
    public void RenderMap(int[,] map)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles(); 
        //Loop through the width of the map
        int width = map.GetUpperBound(0);
        int height = map.GetUpperBound(1);
        int rightIndex = width - 1;
        for (int x = 0; x < width ; x++) 
        {
            //Loop through the height of the map
            for (int y = 0; y < height; y++) 
            {
                // 1 = tile, 0 = no tile
                if (x == 0) 
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), leftTile); 
                } else if (x == rightIndex) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), rightTile); 
                } else {
                    tilemap.SetTile(new Vector3Int(x, y, 0), baseTile); 
                }
            }
        }
    }
}
