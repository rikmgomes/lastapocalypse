using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuInGame : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pauseManager;
    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        volumeSlider.value = AudioListener.volume;
    }

    void Update()
    {
        if(Time.timeScale != 0f)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(panel.activeInHierarchy == false)
                {
                    OpenMenu();
                }
                else{
                    CloseMenu();
                }
            }
        }
        else{
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(panel.activeInHierarchy == false)
                {
                    
                }
                else{
                    CloseMenu();
                }
            }
        }
    }

    public void CloseMenu()
    {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }

    public void OpenMenu()
    {
        pauseManager.PauseGame();
        panel.SetActive(true);
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("menuInicial");
    }

    public void QuitGame()
    {
        Debug.Log("Sair do jogo!");
        Application.Quit();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}