using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCADE : MonoBehaviour
{
    // Array para armazenar os cinco GameObjects
    public GameObject[] objectsToActivate;

    // GameObject da escada
    public GameObject staircase;

    void Update()
    {
        // Verificar se todos os quatro objetos estão ativos
        bool allObjectsActive = true;
        foreach (GameObject obj in objectsToActivate)
        {
            if (!obj.activeSelf)
            {
                allObjectsActive = false;
                break;
            }
        }

        // Ativar a escada se todos os quatro objetos estiverem ativos
        if (allObjectsActive)
        {
            staircase.SetActive(true);
        }
    }
}
