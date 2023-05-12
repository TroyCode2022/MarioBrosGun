using UnityEditor;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementToTarget : MonoBehaviour
{
    Transform target;    // Objeto que queremos perseguir

    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private Vector2[] ultimasPosiciones = new Vector2[5];
    private int indicePosicion = 0;
    private float umbralVariacion = 0.01f; // Umbral para considerar un cambio significativo
    private bool parado;
    private int contSalir = 0;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
#if UNITY_EDITOR
        enabled = !EditorApplication.isPaused;
#else
        enabled = true;
#endif
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }


    private void FixedUpdate()
    {

        if (contSalir == 0)
        {
            indicePosicion = (indicePosicion + 1) % 5;
            ultimasPosiciones[indicePosicion] = (Vector2)transform.position;
            parado = CalcularVariacionPosicion();

            if (parado)
            {
                contSalir = 7;
            }


            direction = (transform.position - target.position).normalized;
            velocity.x = direction.x * speed;
            velocity.y = direction.y * speed;

            if (target.position.x < transform.position.x)
                velocity.x *= -1;
            if (target.position.y < transform.position.y)
                velocity.y *= -1;

            if (direction.x < 0f)
            {
                transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            }
            else if (direction.x > 0f)
            {
                transform.localEulerAngles = Vector3.zero;
            }

            rigidbody.MovePosition(rigidbody.position + direction * velocity * Time.fixedDeltaTime);

        }
        else
            salirDelBloqueo();
        

    }

    private void salirDelBloqueo()
    {
        //Vector2 auxdireccion;
        float umbraldistanciaminima = 1.75f;
        float umbraldistanciamaxima = 3f;
        float distanciaX = Math.Abs(target.position.x - transform.position.x);

        if (distanciaX > umbraldistanciamaxima)
        {
            rigidbody.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
            contSalir--;
        }
        else
        {
            if (target.position.y < transform.position.y && distanciaX <= umbraldistanciaminima)
            {
                if (target.position.x < transform.position.x)
                    rigidbody.AddForce(Vector2.left * 3, ForceMode2D.Impulse);
                else
                    rigidbody.AddForce(Vector2.right * 4, ForceMode2D.Impulse);
                contSalir--;
            }
        }
        
    }

    private bool CalcularVariacionPosicion()
    {
        ultimasPosiciones[indicePosicion] = transform.position;
        

        Vector2 posicionInicial = ultimasPosiciones[0];
        Vector2 posicionActual = (Vector2)transform.position;

        bool haVariado = false;
        for (int i = 1; i < 5; i++)
        {
            if (Vector2.Distance(ultimasPosiciones[i], posicionInicial) > umbralVariacion)
            {
                haVariado = true;
                break;
            }
        }

        return !haVariado;
    }



    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("ENTRA A CHOQUE");
    //    //Vector2 directionaux = transform.position - collision.transform.position;
    //    //Debug.Log(Vector2.Dot(directionaux.normalized, Vector2.up));

    //    if (collision.transform.DotTest(transform, Vector2.up))
    //    {
    //        Debug.Log("Abajo");
    //        EnColision = true;

    //        if (target.position.x < transform.position.x)
    //        {
    //            rigidbody.AddForce(Vector2.left * 500, ForceMode2D.Impulse);
    //            direction = Vector2.left;
    //        }
    //        else
    //        {
    //            rigidbody.AddForce(Vector2.right * 500, ForceMode2D.Impulse);
    //            direction = Vector2.right;
    //        }



    //        velocity = direction * speed;

    //    }
    //    else
    //    {
    //        if ((collision.transform.DotTest(transform, Vector2.left) || collision.transform.DotTest(transform, Vector2.right))
    //            && collision.gameObject.CompareTag("Tuberia"))
    //        {
    //            EnColision = true;
    //            Debug.Log("ACEPTADO");


    //            direction = Vector2.up;
    //            velocity = direction * speed;
    //        }
    //        else
    //        {
    //            if (collision.transform.DotTest(transform, Vector2.down))
    //            {
    //                Debug.Log("Arriba");
    //                EnColision = true;
    //                direction = Vector2.down;
    //                velocity = direction * speed;
    //                rigidbody.AddForce(Vector2.right * 100, ForceMode2D.Impulse);

    //                direction = Vector2.right;
    //                velocity = direction * speed;
    //            }
    //        }
    //    }


    //}

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("SALE DE COLIISON");
    //    EnColision = false;
    //}



}
