using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public int health = 100;

    public void ApplyDamage(int dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
