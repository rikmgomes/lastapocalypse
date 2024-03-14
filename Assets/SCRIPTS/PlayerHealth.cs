using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        recoveryTimer += Time.deltaTime;

        if(recoveryTimer > recoryRate)
        {
           StartCoroutine(RecoveryHealth());
        }
    }

    public void ApplyDamage(int dmg)
    {
        health -= dmg;

        alphaAmount = bloodImage.color;
        alphaAmount.a += ((float)dmg / 100);

        bloodImage.color = alphaAmount;

        if(redImage.color.a < 0.4f)
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
}
