using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager {
    public static float speed = 25f;

    public static void IncreaseSpeed() { 
        speed = 50f;
    }
}
public class Bullet : MonoBehaviour {
    private new Rigidbody2D rigidbody;


    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + transform.right * BulletManager.speed * Time.fixedDeltaTime);
    }

    public static void IncreaseSpeedBullet()
    {
        BulletManager.IncreaseSpeed();
    }

}
