using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile baseTile;
    void Start() { 
        int [,] map = Generate(8, 200, 1);
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
        for (int x = 0; x < map.GetUpperBound(0) ; x++) 
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++) 
            {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1) 
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), baseTile); 
                }
            }
        }
    }
}
