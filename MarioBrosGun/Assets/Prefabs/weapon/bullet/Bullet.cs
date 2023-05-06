using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private new Rigidbody2D rigidbody;

    public static float speed;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        speed = 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        Debug.Log("Hetocad con bala"  + collision.gameObject.name);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Debug.Log("Current speed: " + speed);
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }

    public static void IncreaseSpeed(){ 
        Debug.Log("****INCREMENTANDO VELOCIDAD" );
        speed = 100f;
        }
}
