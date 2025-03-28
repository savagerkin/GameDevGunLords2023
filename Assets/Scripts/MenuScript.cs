using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void loadTut()
    {
        SceneManager.LoadScene(1);

    }
    public void loadGame()
    {
        SceneManager.LoadScene(4);
    }

    public void loadMain()
    {
        SceneManager.LoadScene(0);
    }

    public void endApp()
    {
        Application.Quit();
    }
}
