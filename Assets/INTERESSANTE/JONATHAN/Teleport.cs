using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // O ponto de destino para teletransportar o jogador
    public Transform destination;

    // Quando o jogador entra na área de teletransporte
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou na área é o jogador
        if (other.CompareTag("Player"))
        {
            // Teleporta o jogador para o ponto de destino
            other.transform.position = destination.position;
        }
    }
}
