using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float sceneLoadDelay = 0.5f;

    private readonly string GAME_SCENE = "Game";
    private readonly string MAIN_MENU = "MainMenu";
    private readonly string GAME_OVER = "GameOver";

    public void LoadGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quit Pressed!!");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(GAME_OVER);
    }
}
