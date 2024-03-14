using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coluna : MonoBehaviour
{
    private Animator anim;
    bool tocando;
   

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 5)
        {
            Debug.Log("SAIU DA AREA");
            anim.SetBool("deitado", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
          //anim.SetBool("deitado", true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (TEST.isToutching) { anim.SetBool("deitado", true); }
       
        
    }
}
