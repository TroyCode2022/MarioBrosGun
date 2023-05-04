using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private new Rigidbody2D rigidbody;

    public float speed = 1;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        //Debug.Log("Hetocad con bala"  + collision.gameObject.name);
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }
}
