using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaCorda : MonoBehaviour
{
    public float forcaArremesso = 10f; // Força de arremesso do gancho
    public float forcaPuxao = 20f; // Força de puxão do jogador
    public LayerMask layerMascara; // Máscara de camada para detectar colisões

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
                if (distancia < 2f) // Se a distância entre o gancho e o ponto de fixação for menor que 2f, puxa o jogador
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
