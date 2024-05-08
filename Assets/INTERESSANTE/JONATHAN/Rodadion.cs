using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodadion : MonoBehaviour
{
    // Objeto que será girado
    public GameObject objectToRotate;

    // Luz a ser ativada quando a colisão ocorrer
    public GameObject lightToActivate;

    // Velocidade de rotação
    public float rotationSpeed = 50f;

    // Variável para verificar se a colisão ocorreu
    private bool collisionOccurred = false;

    void Update()
    {
        // Se a colisão ocorreu, girar o objeto em torno do eixo Y
        if (collisionOccurred)
        {
            objectToRotate.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    // Método chamado quando ocorre uma colisão
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar se o objeto colidido possui a tag "battery"
        if (collision.gameObject.CompareTag("Bateria"))
        {
            // Indicar que a colisão ocorreu
            collisionOccurred = true;

            lightToActivate.SetActive(true);
            
        }
    }
}
