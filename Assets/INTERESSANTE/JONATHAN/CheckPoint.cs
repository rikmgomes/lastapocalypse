using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject point; // Ponto espec�fico filho do checkpoint para armazenar a posi��o

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Se o jogador colidir com o checkpoint
        {
            // Armazena a posi��o do ponto filho do checkpoint
            PlayerPrefs.SetFloat("RespawnPointX", point.transform.position.x);
            PlayerPrefs.SetFloat("RespawnPointY", point.transform.position.y);
            PlayerPrefs.SetFloat("RespawnPointZ", point.transform.position.z);
            PlayerPrefs.Save();
        }
    }
}
