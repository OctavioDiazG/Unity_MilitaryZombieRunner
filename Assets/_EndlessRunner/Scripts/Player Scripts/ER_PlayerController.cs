using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_PlayerController : ER_BaseController
{//START CLASS ER_PlayerController

    [Header("Propiedades de disparo")]
    public Transform bulletStartPoint;
    public GameObject bulletPrefab;
    public ParticleSystem shootFX;


    //Variables Privadas
    private Rigidbody RB; //REF COMP Rigidbody del Jugador

    private void Start()
    {//START Start
        //Inicializar la referencia RB
        RB = GetComponent<Rigidbody>();
    }//END Start

    private void Update()
    {//START Update
        //Llamar a la funcion que controla al Jugador con el teclado
        ControlMovementWithKeyboard();

        ChangeRotation();

        ShootingControl();

    }//END Update

    private void FixedUpdate()
    {//START FixedUpdate
        //Llamar al metodo que mueve al jugador
        MovePlayer();
    }//END FixedUpdate

    //Metodo para mover al jugador
    void MovePlayer() 
    {//START MovePlayer
        //Vamos a mover al RB de jugador a una posicion especifica
        //Aqui vamos a usar la posicion actual del RB y le vamos a sumar la velocidad 
        //multiplicada por el tiempo

        //speed es el vector delcarado en la clase ER_BaseController
        RB.MovePosition(RB.position + speed * Time.deltaTime);
    }//END MovePlayer

    //Metodo para controlar al jugador usando el teclado
    void ControlMovementWithKeyboard()
    {//START ControlMovementWithKeyboard
        //Vamos a obtener INPUTS DE TECLADO
        //---------------------------------INPUTS DE TECLAS PRESIONADAS
        //Movimiento a la izquierda
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {//START IF
            //Llamar a la funcion para mover al jugador a la izquierda
            MoveLeft();
        }//END IF

        //Movimiento a la derecha
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {//START IF
            //Llamar a la funcion para mover al jugador a la derecha
            MoveRight();
        }//END IF

        //Aumentar la velocidad
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {//START IF
            //Llamar a la funcion para Acelerar velocidad
            MoveFast();
        }//END IF

        //Disminuir la velocidad
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {//START IF
            //Llamar a la funcion para Reducir velocidad
            MoveSlow();
        }//END IF

        //---------------------------------INPUTS DE TECLAS SOLTADAS
        //Checar si se sueltan las teclas para ir a la izquierda para ir hacia adelante
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) 
        {//START IF
            //Llamar a la funcion para ir hacia adelante
            MoveStraight();
        }//END IF

        //Checar si se sueltan las teclas para ir a la derecha para ir hacia adelante
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {//START IF
            //Llamar a la funcion para ir hacia adelante
            MoveStraight();
        }//END IF

        //Checar si se sueltan las teclas para acelerar para restaurar velocidad
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {//START IF
            //Llamar a la funcion de velocidad normal
            MoveNormal();
        }//END IF

        //Checar si se sueltan las teclas para desacelerar para restaurar velocidad
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {//START IF
            //Llamar a la funcion de velocidad normal
            MoveNormal();
        }//END IF
    }//END ControlMovementWithKeyboard

    void ChangeRotation()
    {
        if(speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);

        }
        else if(speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            GameObject _bullet = Instantiate(bulletPrefab, bulletStartPoint.position, Quaternion.identity);

            _bullet.GetComponent<ER_Bullet>().MoveBullet(2000f);

            shootFX.Play();
            
        }
        
    



    }


}//END CLASS ER_PlayerController
