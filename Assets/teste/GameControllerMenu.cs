using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerMenu : MonoBehaviour
{
    public Button botaoPlay;
    public Button botaoQuit;
    public Color corSelecionado = Color.yellow;
    public Color corNormal = Color.white;
    public string nomeDaCenaDoJogo; // Nome da cena a ser carregada

    private int botaoSelecionado = 0;

    private void Start()
    {
        AtualizarSelecao();
    }

    private void Update()
    {
        // Verifica se o jogador pressionou a tecla para cima
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            botaoSelecionado--; // Move a sele��o para cima
            if (botaoSelecionado < 0)
            {
                botaoSelecionado = 1; // Volta para o �ltimo bot�o se estiver no primeiro
            }
            AtualizarSelecao();
        }
        // Verifica se o jogador pressionou a tecla para baixo
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            botaoSelecionado++; // Move a sele��o para baixo
            if (botaoSelecionado > 1)
            {
                botaoSelecionado = 0; // Volta para o primeiro bot�o se estiver no �ltimo
            }
            AtualizarSelecao();
        }
        // Verifica se o jogador pressionou a tecla Enter
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // Executa a a��o do bot�o selecionado
            if (botaoSelecionado == 0)
            {
                // Bot�o Play selecionado, carrega a cena do jogo
                SceneManager.LoadScene(nomeDaCenaDoJogo);
            }
            else if (botaoSelecionado == 1)
            {
                // Bot�o Quit selecionado, fecha o jogo
                Debug.Log("Sair do jogo");
                Application.Quit(); // Isso s� funciona no build final do jogo
            }
        }
    }

    // Atualiza a apar�ncia visual dos bot�es com base na sele��o atual
    private void AtualizarSelecao()
    {
        if (botaoSelecionado == 0)
        {
            // Selecionar bot�o Play
            botaoPlay.image.color = corSelecionado;
            botaoQuit.image.color = corNormal;
        }
        else
        {
            // Selecionar bot�o Quit
            botaoPlay.image.color = corNormal;
            botaoQuit.image.color = corSelecionado;
        }
    }
}
