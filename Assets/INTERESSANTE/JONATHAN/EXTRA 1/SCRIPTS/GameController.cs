using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;

    public static GameController GC;
    public Image[] images; // Array de imagens
    private int currentIndex = 0; // Índice atual da imagem exibida
    public GameObject fechaInstruction;

    private void Update()
    {
        fecharImage();
    }
    private void Start()
    {
        GC = this;
        
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }
    public void ChangeToScene()
    {
        SceneManager.LoadScene("Indoor Stage 1"); // Carrega a cena com o nome especificado
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarrega a cena atual
    }
    public void fecharImage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentIndex < images.Length - 1)
            {

                // Desativa a imagem atual
                images[currentIndex].gameObject.SetActive(false);
                // Incrementa o índice
                currentIndex++;

            }
            else
            {
                fechaInstruction.SetActive(false);
            }
        }
        
    }
}
