using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToSettingMenu()
    {
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
