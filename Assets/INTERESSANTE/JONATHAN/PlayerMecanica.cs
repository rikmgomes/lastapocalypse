using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMecanica : MonoBehaviour
{
    public Transform pontoDeCarregamento; // Referência ao ponto de transformação onde o objeto será carregado
    public float distanciaMaxima = 2f; // Distância máxima para interagir com a bateria
    private GameObject objetoCarregado; // Objeto que está sendo carregado
    private Rigidbody objetoRigidbody; // Rigidbody do objeto carregado
    private bool estaCarregando = false; // Verifica se o jogador está carregando um objeto
    private Vector3 ultimoCheckpoint;
    public string VerificaID;

    private void Start()
    {
        Respawn();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Se o jogador pressionar o botão esquerdo do mouse
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, distanciaMaxima))
            {
                if (hit.collider.CompareTag("Bateria")) // Se o objeto atingido tiver a tag "Bateria"
                {
                    if (!estaCarregando) // Se o jogador não estiver carregando um objeto
                    {
                        CarregarObjeto(hit.collider.gameObject);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) // Se o jogador soltar o botão esquerdo do mouse
        {
            if (estaCarregando) // Se o jogador estiver carregando um objeto
            {
                LargarObjeto();
            }
        }
    }

    void CarregarObjeto(GameObject objeto)
    {
        objetoCarregado = objeto;
        objetoRigidbody = objetoCarregado.GetComponent<Rigidbody>(); // Obtém o Rigidbody do objeto carregado
        objetoRigidbody.isKinematic = true; // Ativa o isKinematic enquanto estiver sendo carregado
        objetoCarregado.transform.SetParent(pontoDeCarregamento);
        objetoCarregado.transform.localPosition = Vector3.zero;
        estaCarregando = true;
    }

    void LargarObjeto()
    {
        objetoRigidbody.isKinematic = false; // Desativa o isKinematic quando o objeto é largado
        objetoCarregado.transform.SetParent(null);
        objetoCarregado = null;
        estaCarregando = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GAMEOVER"))
        {
            Morrer();
        }
    }
    public void Morrer()
    {
        // Move o jogador de volta para o último checkpoint alcançado
        Respawn();

    }
    public void Respawn()
    {
        // Obtém a posição de respawn armazenada
        Vector3 respawnPosition = new Vector3(
            PlayerPrefs.GetFloat("RespawnPointX", transform.position.x),
            PlayerPrefs.GetFloat("RespawnPointY", transform.position.y),
            PlayerPrefs.GetFloat("RespawnPointZ", transform.position.z)
        );

        // Move o jogador para a posição de respawn
        transform.position = respawnPosition;
    }
}
