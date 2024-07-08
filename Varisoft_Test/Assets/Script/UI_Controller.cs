using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] private Health_UI healthUI;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinnerPanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Player player;
    private int stickCount;

    public void Init(Player _player)
    {
        player = _player;
        healthUI.Init(player.GetComponent<Health>());

        playerPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWinnerPanel.SetActive(false);

        stickCount = GameManager.Instance.MaxEnemies;
        scoreText.text = stickCount.ToString();
    }

    public void CheckRemaining()
    {
        stickCount--;
        scoreText.text = stickCount.ToString();

        if(stickCount <= 0)
        {
            GameManager.Instance.IsGamePlay = false;
            Win();
        }
    }

    public void Retry() 
    {
        playerPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    } 
    public void Win() 
    {
        playerPanel.SetActive(false);
        gameWinnerPanel.SetActive(true);
    } 
    public void Retry_Button() => SceneManager.LoadScene(1);

    public void Exit() => Application.Quit();
}
