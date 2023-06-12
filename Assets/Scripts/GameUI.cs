using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private int killingStreak;

    private Text text;
    private GameObject score;
    private GameObject restartMenu;
    private ZombieSpawner spawner;

    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("KillStreak");

        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<ZombieSpawner>();
        text = score.GetComponent<Text>();

        restartMenu = GameObject.FindGameObjectWithTag("UIRestart");
        restartMenu.SetActive(false);
    }

    private void Update()
    {
        UpdateScore();
    }

    public void IncreaseScore(int count)
    {
        killingStreak += count;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GetReloadMenu()
    {
        restartMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void UpdateScore()
    {
        killingStreak = spawner.KilledZombieCount;
        text.text = killingStreak.ToString();
    }

}
