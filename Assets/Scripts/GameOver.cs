using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    #region Default Values
    [Header("Go to menu")]
    public string menuScene = "Menu";
    public string gameScene = "Main";
    public Color loadToColor = Color.black;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Menu Mouse Clicks
    public void MouseClick(string buttonType)
    {
        if (buttonType == "Restart")
        {
            Initiate.Fade(gameScene, loadToColor, 1.0f);
        }
        else if (buttonType == "Menu")
        {
            Initiate.Fade(menuScene, loadToColor, 1.0f);
        }
    }
    #endregion
}
