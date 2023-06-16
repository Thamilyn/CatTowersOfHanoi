using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void PlayLevelOne()
    {
        SceneManager.LoadScene("MA02_Level1");
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene("MA03_MainPuzzle");
    }

    public void PlayLevelThree()
    {
        SceneManager.LoadScene("MA04_Level3");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
