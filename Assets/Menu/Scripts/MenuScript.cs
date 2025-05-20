using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        //scene index 1 is the scene of the game
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        //implement application.quit??
        Application.Quit();
    }


}
