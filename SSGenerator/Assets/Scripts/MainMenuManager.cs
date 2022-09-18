using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private static string GAME_NAME = "Main";
    private string levelName;

    //[SerializeField]
    //private GameObject loadingScreen;

    public void StartGame()
    {
        levelName = GAME_NAME;

        StartCoroutine(LoadLevelWithNameCo());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        levelName = "Start";

        StartCoroutine(LoadLevelWithNameCo());
    }

    IEnumerator LoadLevelWithNameCo()
    {
        //loadingScreen.SetActive(true);        

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(levelName);

        yield return new WaitForSeconds(0.5f);

        //loadingScreen.SetActive(false);
    }
}
