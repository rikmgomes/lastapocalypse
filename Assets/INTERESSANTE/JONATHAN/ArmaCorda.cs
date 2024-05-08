using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaCorda : MonoBehaviour
{
    public float forcaArremesso = 10f; // For�a de arremesso do gancho
    public float forcaPuxao = 20f; // For�a de pux�o do jogador
    public LayerMask layerMascara; // M�scara de camada para detectar colis�es

    private Rigidbody rb;
    private bool lancado = false;
    private Vector3 pontoFixacao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!lancado && Input.GetMouseButtonDown(0))
        {
            LaunchGancho();
        }
    }

    void LaunchGancho()
    {
        Vector3 direcao = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.AddForce(direcao * forcaArremesso, ForceMode.Impulse);
        lancado = true;
    }

    void FixedUpdate()
    {
        if (lancado)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMascara))
            {
                pontoFixacao = hit.point;
                float distancia = Vector3.Distance(transform.position, pontoFixacao);
                if (distancia < 2f) // Se a dist�ncia entre o gancho e o ponto de fixa��o for menor que 2f, puxa o jogador
                {
                    PuxarJogador();
                }
            }
        }
    }

    void PuxarJogador()
    {
        Vector3 direcao = (transform.position - pontoFixacao).normalized;
        rb.AddForce(direcao * forcaPuxao, ForceMode.Impulse);
    }
}
