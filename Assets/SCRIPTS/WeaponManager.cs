using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    private int index;
    public float switchDalay = 1f;
    private bool isSwitching;

    // Start is called before the first frame update
    void Start()
    {
        initializeWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator switchWeaponDelay(int newIndex)
    {
        isSwitching = true;
        yield return new WaitForSeconds(switchDalay);
        isSwitching = false;
        switchWeapons(newIndex);
    }

    //desativa as armas
    void initializeWeapons()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        //weapons[0].SetActive(true);
    }

    //habilita uma arma
    void switchWeapons(int idx)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[idx].SetActive(true);
    }
}
