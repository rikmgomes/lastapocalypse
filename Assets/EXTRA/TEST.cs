using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public static bool isToutching;
    public static TEST teste;
    public float ColliderRadius;
    public AudioClip clip;
    private AudioSource sorc;
    public AudioListener audioLLisstener;
    public GameObject cub1;
    public GameObject cub2;

    private void Start()
    {
        teste = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            SONG();
        }
        else
        {
            audioLLisstener.gameObject.SetActive(false);
            isToutching = false;
            cub1.SetActive(false);
            cub2.SetActive(true);
        }
       
    }
    public bool IsTOUCHING()
    {
        return isToutching;
    }
    private void SONG()
    {
        cub1.SetActive(true);
        cub2.SetActive(false);
        audioLLisstener.gameObject.SetActive(true);
        isToutching = true;
        //foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * ColliderRadius), ColliderRadius))
        //{

        //}
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, ColliderRadius);
    }
}
