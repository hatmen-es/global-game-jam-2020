using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : Singleton<SaveManager>
{
    public string sceneName = "";
    public Animator PanelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PanelAnimator.SetTrigger("Fade");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        FadeIntNewScene(sceneName);
    }

    private void FadeIntNewScene(string name)
    {
        PanelAnimator.SetTrigger("Idle");
        StartCoroutine(ChangeSceneTo(name));
    }

    private IEnumerator ChangeSceneTo(string name)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);
    }
}
