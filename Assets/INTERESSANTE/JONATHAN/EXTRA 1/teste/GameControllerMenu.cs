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
            botaoSelecionado--; // Move a seleção para cima
            if (botaoSelecionado < 0)
            {
                botaoSelecionado = 1; // Volta para o último botão se estiver no primeiro
            }
            AtualizarSelecao();
        }
        // Verifica se o jogador pressionou a tecla para baixo
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            botaoSelecionado++; // Move a seleção para baixo
            if (botaoSelecionado > 1)
            {
                botaoSelecionado = 0; // Volta para o primeiro botão se estiver no último
            }
            AtualizarSelecao();
        }
        // Verifica se o jogador pressionou a tecla Enter
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // Executa a ação do botão selecionado
            if (botaoSelecionado == 0)
            {
                // Botão Play selecionado, carrega a cena do jogo
                SceneManager.LoadScene(nomeDaCenaDoJogo);
            }
            else if (botaoSelecionado == 1)
            {
                // Botão Quit selecionado, fecha o jogo
                Debug.Log("Sair do jogo");
                Application.Quit(); // Isso só funciona no build final do jogo
            }
        }
    }

    // Atualiza a aparência visual dos botões com base na seleção atual
    private void AtualizarSelecao()
    {
        if (botaoSelecionado == 0)
        {
            // Selecionar botão Play
            botaoPlay.image.color = corSelecionado;
            botaoQuit.image.color = corNormal;
        }
        else
        {
            // Selecionar botão Quit
            botaoPlay.image.color = corNormal;
            botaoQuit.image.color = corSelecionado;
        }
    }
}
