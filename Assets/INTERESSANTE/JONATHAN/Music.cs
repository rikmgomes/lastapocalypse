using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Referência para o áudio source que vai tocar a música
    public AudioSource audioSource;

    // Referência para a música que queremos reproduzir em loop
    public AudioClip backgroundMusic;

    // Executa uma vez quando o script é iniciado
    private void Start()
    {
        // Define a música de fundo e configura para tocar em loop
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;

        // Inicia a reprodução da música de fundo
        audioSource.Play();
    }
}
