using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winner : MonoBehaviour
{
    // Nome da cena para a qual voc� quer mudar
    public string sceneName;

    // Fun��o chamada quando outro collider entra na �rea de gatilho
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no gatilho � o jogador (assumindo que o jogador tem a tag "Player")
        if (other.CompareTag("Player"))
        {
            // Carrega a nova cena
            SceneManager.LoadScene(sceneName);
        }
    }
}
