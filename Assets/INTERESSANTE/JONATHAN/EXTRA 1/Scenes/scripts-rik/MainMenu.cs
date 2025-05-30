using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject canvasTutorial;
    [SerializeField] private GameObject canvasLore;
    [SerializeField] private GameObject canvasMenuInicial;
    [SerializeField] Slider volumeSlider;
    public AudioSource audioClip;

    private void Awake()
    {
        volumeSlider.value = AudioListener.volume;
    }

    public void playButton()
    {
        audioClip.Play();
    }

    public void AbrirTutoras()
    {
        canvasMenuInicial.SetActive(false);
        canvasTutorial.SetActive(true);
    }

    public void AbrirLore()
    {
        canvasTutorial.SetActive(false);
        canvasLore.SetActive(true);
    }

    public void AbrirTowerIron()
    {
        SceneManager.LoadScene("TowerIron");
    }

    public void Options()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void CloseOptions()
    {
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
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
