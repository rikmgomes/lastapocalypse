using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ACESSAR CANVAS DE INTERFACE
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header ("Propriets")]
    //variavel para controlar o alcance maximo que as balas vão chegar.
    public float range = 100f;
    //tamanho do pente, o total de munição que a arma carrega
    public int totalBullets = 40;
    //numero de balas do seu pent atual
    public int currentBuleets;
    //total de balas que vc tem, usar para a logica de recarregar
    public int BulletLeft = 100;

    public float spreadFactor;

    // controlar o tempo entre um disparo e outro
    public float fireRate = 0.1f;

    private float fireTimer;

    public Transform shootPonit;
    public ParticleSystem fireEffect;
    public AudioClip shootSound;
    private AudioSource audioSource;

    public GameObject hitEffect;
    public GameObject bulletImpact;

    private Animator anim;
    public int damage;

    //para saber se o player esta recarregando
    private bool isReloading;
    
    public enum ShootMode
    {
        Auto,
        Semi
    }

    public ShootMode shootMode;
    private bool shootInput;

    [Header("Aim Config")]

    private Vector3 originalPos;
    public Vector3 aimPos;
    public float aimSpeed;

    [Header("UI")]
    public Text ammoText;

    private void OnEnable()
    {
        updateAmmoText(); // atualizar o texto de munição
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        currentBuleets = totalBullets;
        audioSource = GetComponent<AudioSource>();
        updateAmmoText();
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButton("Fire1"))
        //{
        //    if(currentBuleets > 0)
        //    {
        //        //executar tiro
        //        Fire();

        //    }
        //    else if(BulletLeft > 0) // se eu ainda tenho balas eu recarrego
        //    {
        //        DoReload();
        //    }

        //}

        switch (shootMode)
        {
            case ShootMode.Auto:

                shootInput = Input.GetButton("Fire1");

                break;

            case ShootMode.Semi:

                shootInput = Input.GetButtonDown("Fire1");

                break;
        }

        if (shootInput)
        {
            if (currentBuleets > 0)
            {
                //executar tiro
                Fire();

            }
            else if (BulletLeft > 0) // se eu ainda tenho balas eu recarrego
            {
                DoReload();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))//recarregar
        {
            if(currentBuleets < totalBullets && BulletLeft > 0)
            {
                DoReload();
            }
        }
        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        //mira
        ToAim();
    }
    private void Fire()
    {
        if (fireTimer < fireRate || isReloading || currentBuleets <= 0)
        {
            return;
        }

        RaycastHit hit;

        Vector3 shootDirection = shootPonit.transform.forward;

        //para deixar o tiro espalhado
        shootDirection = shootDirection + shootPonit.TransformDirection(new Vector3(Random.Range(-spreadFactor,spreadFactor), Random.Range(-spreadFactor, spreadFactor)));

        //outra forma de fazer o tiro espalhado-------------------------------------
        //shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
        //shootDirection.y += Random.Range(-spreadFactor, spreadFactor);
        //-------------------------------------------------------------------------

        //esse if tranforma uma linha reta de detecção como se fosse um infravermelho
        //primeira informação é de onde vai surger / direção / objeto que bateu ou colidiu/tamanho / + opcional que é a layer
        // o objeto que for acertado é armazenado no hit
        if (Physics.Raycast(shootPonit.position,shootDirection, out hit,range))
        {
            //Debug.Log(hit.transform.name);
            GameObject hitParticle = Instantiate(hitEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            GameObject bullet = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

            //quando a bala é acertada ela vai para dentro do objeto na hierarquia
            bullet.transform.SetParent(hit.transform);

            Destroy(hitParticle, 1f);
            Destroy(bullet, 3f);


            //se acertar algum componente que tenha esse script, faz alguma coisa
            if (hit.transform.GetComponent<ObjectHealth>())
            {
                hit.transform.GetComponent<ObjectHealth>().ApplyDamage(damage);
            }
            if (hit.transform.GetComponent<Soldier>())
            {
                Destroy(bullet);
                hit.transform.GetComponent<Soldier>().ApplyDamage(damage);
            }
        }


        //permiti chamar uma transição e aduração dela diretmente (has exit time)
        anim.CrossFadeInFixedTime("Fire", 0.01f);
        fireEffect.Play(); // ativando a particulação de animação
        updateAmmoText(); // atualizar o texto de munição
        PlayShootSound();
        currentBuleets--;
        fireTimer = 0f;
    }

    public void ToAim()
    {
        if (Input.GetButton("Fire2") && !isReloading)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * aimSpeed);
        }
        else
        {
            //para voltar ao normal e não ficar sempre mirando
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * aimSpeed);
        }
    }

    private void FixedUpdate()
    {
        //armazenando o animator state na variavel info
        //animator state é onde editamos as animações
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        //recebendo o valor da execução da animação no animator
        //se executada = true
        //ao contrario = false
        isReloading = info.IsName("Reload");
    }
    //executar a animação de recarrregando a arma
    void DoReload()
    {
        //se ja estiver recarregando não pode recarregar enquanto recarrega
        if (isReloading)
        {
            return;
        }
        anim.CrossFadeInFixedTime("Reload", 0.01f);
        updateAmmoText(); // atualizar o texto de munição
    }
    public void Reload()//faz os calculos
    {
        if(BulletLeft <= 0)
        {
            return;
        }

        //quantidade de balas que o personagem vai recarregar
        int bulletsToLoad = totalBullets - currentBuleets;

        //balas que vamos deduzir
        int bulletsToDeduct = (BulletLeft >= bulletsToLoad) ? bulletsToLoad : BulletLeft;

        BulletLeft -= bulletsToDeduct;
        currentBuleets += bulletsToDeduct;
        updateAmmoText(); // atualizar o texto de munição
    }
    void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
    void updateAmmoText()
    {
        ammoText.text = currentBuleets + " / " + BulletLeft;
    }
}
