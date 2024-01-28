using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : UIManager
{
    private GameObject pauseScreen;

    private bool isPaused;

    [SerializeField] private Slider healthSliderBar;
    [SerializeField] private Slider staminaSliderBar;

    [SerializeField] private HealthSystem playerHealthSystem;

    private void Start()
    {
        pauseScreen = GameObject.Find("PauseScreen");

        isPaused = false;
        if (pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }

        AudioManager.instance.PlayBGM();

        healthSliderBar.maxValue = playerHealthSystem.maxHealth;
        staminaSliderBar.maxValue = playerHealthSystem.maxStamina;

        healthSliderBar.value = playerHealthSystem.currentHealth;
        staminaSliderBar.value = playerHealthSystem.currentStamina;
    }

    private void Update()
    {
        if (pauseScreen != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnpausePauseGame();
            }
        }

        HealthVisualise();
        StaminaVisualise();
    }

    public void UnpausePauseGame()
    {
        if (isPaused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainTitle");
    }

    public void HealthVisualise()
    {
        healthSliderBar.value = playerHealthSystem.currentHealth;
    }

    public void StaminaVisualise()
    {
        staminaSliderBar.value = playerHealthSystem.currentStamina;
    }
}
