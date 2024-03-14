using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwei : MonoBehaviour
{
    public float MinAmount;
    public float MaxAmount;
    public float smoothAmount;

    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        //position em rela��o ao mundo
        //local position em rela��o ao pai do objeto
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Recebe a rota��o do mouse no eixo X
        //negativo para gerar um efeito de gravidade na m�o e ela ir oposto a dire��o do mouse
        float moveX = -Input.GetAxis("Mouse X") * MinAmount;
        //Recebe a rota��o do mouse no eixo Y
        float moveY = -Input.GetAxis("Mouse Y") * MinAmount;

        moveX = Mathf.Clamp(moveX, -MaxAmount, MaxAmount);
        moveY = Mathf.Clamp(moveY, -MaxAmount, MaxAmount);

        //armazenar as 2 variaveis de cima
        Vector3 finalPos = new Vector3(moveX, moveY, 0f);

        // lerp retorna um valor liear entre 2eixos, o movimento n�o vai de uma vez, vai devagar
        //retorna o movimento entre 2 objetos, do ponto A ate o ponto B
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initialPos, Time.deltaTime * smoothAmount);
    }
}
