using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI endGameText;
    public Button exitButton,pauseButton,resumeButton;
    [SerializeField] public GameObject pausePanel,gameWonPanel;
    [SerializeField] public TextMeshProUGUI _timeText;
    [SerializeField] float _time = 50;
    private List<GameObject> enemies = new List<GameObject>();
    private void Start()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.AddRange(enemyObjects);
    }
    private void Update()
    {
        TimeUpdate();
    }
    public void TimeUpdate()
    {
        _time -= Time.deltaTime;
        _timeText.text = "Time = " + ((int)_time);
        if (_time <= 0)
        {
            Time.timeScale = 0;
            gameWonPanel.SetActive(true);

        }
    }
    public void EndGame(bool playerWon)
    {
        // Oyun bittiðinde çaðrýlacak fonksiyon
        if (playerWon)
        {
            // Oyuncu kazandýysa
            endGameText.text = "Oyun Bitti - Kazandýn!";
        }
        else
        {
            // Oyuncu kaybetti veya düþmanlar bitti
            endGameText.text = "Oyun Bitti - Kaybettin!";
        }

        // UI elemanlarýný göster
        gameWonPanel.SetActive(true);

        // Oyunu dondur
        Time.timeScale = 0f;
    }
    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (enemies.Count == 0)
        {
            EndGame(true); // Düþmanlar bittiðinde oyunu kazandýr
        }
    }
    public void PauseButtonOnClick()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeButtonOnClick()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
