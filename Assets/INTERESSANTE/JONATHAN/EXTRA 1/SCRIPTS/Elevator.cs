using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 2.0f; // Velocidade de movimento do elevador
    public Transform pointA; // Ponto m�nimo do elevador
    public Transform pointB; // Ponto m�ximo do elevador

    private bool movingUp = true; // Flag para verificar se o elevador est� se movendo para cima

    private void Update()
    {
        // Move o elevador para cima ou para baixo dependendo da dire��o
        if (movingUp)
        {
            MoveTo(pointB.position);
            if (transform.position.y >= pointB.position.y)
                movingUp = false;
        }
        else
        {
            MoveTo(pointA.position);
            if (transform.position.y <= pointA.position.y)
                movingUp = true;
        }
    }

    private void MoveTo(Vector3 targetPosition)
    {
        // Move o elevador para o ponto de destino
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    // Retorna o ponto m�nimo do elevador
    public Vector3 GetPointA()
    {
        return pointA.position;
    }

    // Retorna o ponto m�ximo do elevador
    public Vector3 GetPointB()
    {
        return pointB.position;
    }
}
