using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Image bloodImage;
    public Image redImage;

    private Color alphaAmount;

    public int recoveryFactor = 20;
    private float recoveryTimer;
    public float recoryRate = 5f;

    public bool isDead;

    public GameObject interactMessage; // Objeto da mensagem de intera��o
    private bool isInteracting = false; // Flag para verificar se o jogador est� interagindo
    private GameObject lastColecionavel; // Refer�ncia para o �ltimo objeto colecion�vel olhado
    private GameObject lastMensagem;


    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        recoveryTimer += Time.deltaTime;

        if (recoveryTimer > recoryRate)
        {
            StartCoroutine(RecoveryHealth());
        }
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        DetectingObjetct();
    }

    public void ApplyDamage(int dmg)
    {
        health -= dmg;

        alphaAmount = bloodImage.color;
        alphaAmount.a += ((float)dmg / 100);

        bloodImage.color = alphaAmount;

        if (redImage.color.a < 0.4f)
        {
            redImage.color = new Color(255f, 0f, 0f, alphaAmount.a);
        }


        if (health <= 0)
        {
            GameController.GC.ShowGameOver();
            isDead = true;
            Debug.Log("GAME OVER");
        }

        //se tomar dano
        recoveryTimer = 0f;
    }

    IEnumerator RecoveryHealth()
    {
        while (health < maxHealth)
        {
            health += recoveryFactor;

            alphaAmount.a -= ((float)recoveryFactor / 100);
            bloodImage.color = alphaAmount;
            redImage.color = new Color(255f, 0f, 0f, alphaAmount.a);
            yield return new WaitForSeconds(1.5f);
        }

    }
    void DetectingObjetct()
    {
        // Raycast no centro da tela para detectar objetos colecion�veis
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Verifica se o jogador est� olhando para um objeto colecion�vel
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("coletavel"))
        {
            // Pega o objeto colecion�vel e sua mensagem
            GameObject colecionavel = hit.collider.gameObject;
            GameObject mensagem = colecionavel.transform.Find("Mensagem")?.gameObject;

            // Ativa a mensagem se ela existir e ainda n�o estiver ativa
            if (mensagem != null && !mensagem.activeSelf)
            {
                mensagem.SetActive(true);

                // Mant�m a mensagem sempre olhando para o jogador
                Vector3 lookAtPosition = Camera.main.transform.position;
                lookAtPosition.y = mensagem.transform.position.y;
                mensagem.transform.LookAt(lookAtPosition);
            }

            // Verifica se o jogador pressionou a tecla E para interagir
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Destroi o objeto colecion�vel atual
                Destroy(colecionavel);
            }

            // Atualiza as refer�ncias para o �ltimo objeto colecion�vel e sua mensagem
            lastColecionavel = colecionavel;
            lastMensagem = mensagem;
        }
        else
        {
            // Se o jogador n�o estiver olhando para um objeto colecion�vel,
            // desativa a �ltima mensagem de objeto colecion�vel olhado
            if (lastMensagem != null && lastColecionavel != null)
            {
                lastMensagem.SetActive(false);
            }


        }
    }
}
