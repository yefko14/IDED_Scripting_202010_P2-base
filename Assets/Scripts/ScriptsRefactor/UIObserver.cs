using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIObserver : MonoBehaviour
{
    [SerializeField] PlayerRefactor player;

    [SerializeField]
    private Image[] lifeImages;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Button restartBtn;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        OnPlayerReceivedPoints(0);
        ToggleRestartButton(false);
        if (player != null)
        {
            player.onPlayerHit += new PlayerRefactor.OnPlayerHit(OnPlayerReceivedDamage);
            player.onPlayerScoreChanged += new PlayerRefactor.OnPlayerScoreChanged(OnPlayerReceivedPoints);
            player.onPlayerDied += new PlayerRefactor.OnPlayerDied(OnPlayerDied);
        }
    }
    private void ToggleRestartButton(bool val)
    {
        if (restartBtn != null)
        {
            restartBtn.gameObject.SetActive(val);
        }
    }
    void OnPlayerReceivedDamage(int delta)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (lifeImages[i] != null && lifeImages[i].enabled)
            {
                lifeImages[i].gameObject.SetActive(player.Lives >= i + 1);
            }
        }
    }
    void OnPlayerReceivedPoints(int delta)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = player.Score.ToString();
        }
    }
    void OnPlayerDied()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "Game Over";
        }

        ToggleRestartButton(true);
    }
}
