using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject loading;

    private void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("UIMainMenu");
        loading = GameObject.FindGameObjectWithTag("UILoading");
        loading.SetActive(false);
    }

    public void StartLevel1()
    {
        loading.SetActive(true);
        mainMenu.SetActive(false);
        Invoke("LoadLevel1", 2);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
