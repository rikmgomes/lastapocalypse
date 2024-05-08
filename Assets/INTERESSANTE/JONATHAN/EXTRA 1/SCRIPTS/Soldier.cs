using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    //controlar as animações do personagem
    private Animator anim;
    //controlar o NAVMASH do persoangem, que é por onde ele pode andar
    private NavMeshAgent NavMash;

    //referenciando o personagem principal, oque controlamos
    private GameObject Player;

    //controlar a area de distancia que o inimigo vai atacar
    public float atkDistance = 10f;

    //distancia maxima que o inimigo pode perseguir o persongem
    public float followDistance = 20f;

    //probabilidade de chances do inimigo acertar o personagem
    public float atkProbality;

    //dano que ele tira
    public int damage = 20;
    //vida do inimigo
    public int health = 100;

    public float range = 100f;

    public Transform shootPoint;

    public float fireRate = 0.5f;
    private float fireTime;

    public ParticleSystem fireEffect;
    public AudioClip shootAudio;
    private AudioSource audioSouce;

    private bool isDead;
    private PlayerHealth playerHealths;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        NavMash = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        audioSouce = GetComponent<AudioSource>();
        playerHealths = Player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NavMash.enabled && playerHealths.isDead == false)
        {
            
            //retorna um valor entre 2 pontos
            float dist = Vector3.Distance(Player.transform.position, transform.position);
            //se ta atirando
            bool shoot = false;
            //vendo se a distancia do player é menor que a distancia de visão
            bool follow = (dist < followDistance);

            

            if (follow)
            {
                Debug.Log("ta na visao");
                if(dist < atkDistance)
                {

                    shoot = true;
                    Fire();
                    Debug.Log("atirar");
                }

                NavMash.SetDestination(Player.transform.position);
                //transform.LookAt(Player.transform);
                transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y - 0.5f, Player.transform.position.z));
                shootPoint.LookAt(Player.transform.position);
            }

            if(!follow || shoot)
            {
                NavMash.SetDestination(transform.position);
            }

            anim.SetBool("shoot", shoot);
            anim.SetBool("run", follow);
        }

        if(fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }
    }

    public void Fire()
    {
        if(fireTime < fireRate)
        {
            return;
        }

        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position,shootPoint.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            //se o objeto que estou acertando tem o componente player health
            if (hit.transform.GetComponent<PlayerHealth>())
            {
                hit.transform.GetComponent<PlayerHealth>().ApplyDamage(damage);
            }
        }

        // ativar o efeito de particulas q simula o fogo do tiro
        fireEffect.Play();
        PlayShootAudio();
        fireTime = 0f;
    }
    public void ApplyDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0 && !isDead)
        {
            NavMash.enabled = false;
            anim.SetBool("shoot", false);
            anim.SetBool("run", false);
            anim.SetTrigger("die");
            isDead = true;

        }
    }
    public void PlayShootAudio()
    {
        audioSouce.PlayOneShot(shootAudio);
    }
}
