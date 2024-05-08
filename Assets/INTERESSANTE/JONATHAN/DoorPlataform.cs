using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlataform : MonoBehaviour
{
    public GameObject objetoAtivar; // Objeto a ser ativado quando a bateria for colocada em cima
    public GameObject objetoDesativar; // Objeto a ser desativado quando a bateria for colocada em cima

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bateria")) // Se a bateria colidir com a plataforma
        {
            objetoAtivar.SetActive(true); // Ativa o objeto referenciado
            objetoDesativar.SetActive(false); // Desativa o objeto referenciado
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bateria")) // Se a bateria colidir com a plataforma
        {
            objetoAtivar.SetActive(false); // Ativa o objeto referenciado
            objetoDesativar.SetActive(true); // Desativa o objeto referenciado
        }
    }

}
