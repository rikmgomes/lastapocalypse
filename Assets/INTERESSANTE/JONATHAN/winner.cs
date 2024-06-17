using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winner : MonoBehaviour
{
    // Nome da cena para a qual você quer mudar
    public string sceneName;

    // Função chamada quando outro collider entra na área de gatilho
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no gatilho é o jogador (assumindo que o jogador tem a tag "Player")
        if (other.CompareTag("Player"))
        {
            // Carrega a nova cena
            SceneManager.LoadScene(sceneName);
        }
    }
}
