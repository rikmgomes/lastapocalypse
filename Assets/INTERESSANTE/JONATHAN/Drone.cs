using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Transform[] patrolPoints; // Pontos de patrulha
    public float moveSpeed = 3f; // Velocidade de movimento
    public float detectionRadius = 5f; // Raio de detecção

    private int currentPatrolIndex = 0; // Índice do ponto de patrulha atual
    private Transform player; // Referência ao jogador
    bool playerDetect = false;

    void Update()
    {
        if (!player)
        {
            Patrol(); // Realiza a patrulha
        }
        
        DetectPlayer(); // Detecta o jogador
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("Nenhum ponto de patrulha definido para o drone!");
            return;
        }

        // Move em direção ao próximo ponto de patrulha
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Verifica se chegou ao ponto de patrulha
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Avança para o próximo ponto de patrulha
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player = collider.transform;
                // Se detectar o jogador, vai em direção a ele
                transform.LookAt(player.position);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                playerDetect = true;
                //Debug.Log("achou");
                break;
            }
            else
            {
                playerDetect = false;
                Patrol();
                //Debug.Log("Fora de alcance");
            }
        }
    }

    // Visualização da área de detecção no editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
