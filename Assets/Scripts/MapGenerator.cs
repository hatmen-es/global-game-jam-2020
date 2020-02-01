using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static int[,] Generate(int width, int height, int nElements)
    {
        int[,] map = new int[width, height];
        if (gameControllerScript.instance.gameOver)
            return map;
        for (int x = 0; x < height; x++) {
            bool isElementSet = false;
            for (int y = 0; y < width; y++) {
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
}
