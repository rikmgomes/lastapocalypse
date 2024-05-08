using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Refer�ncia para o �udio source que vai tocar a m�sica
    public AudioSource audioSource;

    // Refer�ncia para a m�sica que queremos reproduzir em loop
    public AudioClip backgroundMusic;

    // Executa uma vez quando o script � iniciado
    private void Start()
    {
        // Define a m�sica de fundo e configura para tocar em loop
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;

        // Inicia a reprodu��o da m�sica de fundo
        audioSource.Play();
    }
}
