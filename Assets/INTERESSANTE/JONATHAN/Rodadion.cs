using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodadion : MonoBehaviour
{
    // Objeto que ser� girado
    public GameObject objectToRotate;

    // Luz a ser ativada quando a colis�o ocorrer
    public GameObject lightToActivate;

    // Velocidade de rota��o
    public float rotationSpeed = 50f;

    // Vari�vel para verificar se a colis�o ocorreu
    private bool collisionOccurred = false;

    void Update()
    {
        // Se a colis�o ocorreu, girar o objeto em torno do eixo Y
        if (collisionOccurred)
        {
            objectToRotate.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    // M�todo chamado quando ocorre uma colis�o
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar se o objeto colidido possui a tag "battery"
        if (collision.gameObject.CompareTag("Bateria"))
        {
            // Indicar que a colis�o ocorreu
            collisionOccurred = true;

            lightToActivate.SetActive(true);
            
        }
    }
}
