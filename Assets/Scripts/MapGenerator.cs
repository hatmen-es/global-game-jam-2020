using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap borderTilemap;
    public Tilemap fixableTilemap;
    public Tile baseTile;
    public Tile leftTile;
    public Tile rightTile;
    public Tile borderLeftTile;
    public Tile borderRightTile;
    public Tile goalTile;
    public int height = 200;
    public int width = 14;
    public int spaceBetween = 3;
    private int[,] map; 
    void Start() { 
        map = Generate(height, 30);
        RenderMap(map);
    }
    public int[,] Generate(int height, int safeInterval)
    {
        int nElements = 17;
        int[,] map = new int[width, height];
        int rightIndex = width - 1;

        for (int y = 0; y < height; y++) {
            int fixablePos = 0;
            if (y % spaceBetween == 0) {
                fixablePos = Random.Range(1, width);
            }
            for (int x = 0; x < width; x++) {
                if (y < safeInterval) {
                    map[x, y] = 0;
                } else if (x == 0 || x == rightIndex) {
                    map[x, y] = 0;
                } else if (x == fixablePos) {
                    map[x, y] = Random.Range(1, nElements);
                } else {
                    map[x, y] = 0;
                }
            }
        }
        return map;
    }
    public void RenderMap(int[,] map)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles(); 
        borderTilemap.ClearAllTiles(); 
        fixableTilemap.ClearAllTiles(); 
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
                    borderTilemap.SetTile(new Vector3Int(x, y, 0), borderLeftTile); 
                } else if (x == rightIndex) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), rightTile); 
                    borderTilemap.SetTile(new Vector3Int(x, y, 0), borderRightTile); 
                } else if(map[x,y] != 0) {
                    fixableTilemap.SetTile(new Vector3Int(x, y, 0), goalTile); 
                } else {
                    tilemap.SetTile(new Vector3Int(x, y, 0), baseTile); 
                }
            }
        }
    }

    public void generatNextMap(int threshold) {
        int[,] next = Generate(height - threshold, 0);
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (y < threshold) {
                    map[x,y] = map[x, y + threshold];
                } else {
                    map[x,y] = next[x, y - threshold];
                }
            }
        }
        RenderMap(map);
    }

    public int mapElementInWorldPos(Vector3 pos) {
        Vector3Int matrixPos = tilemap.WorldToCell(pos);
        return map[matrixPos.x, matrixPos.y];
    }
}
