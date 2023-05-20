using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager {
    public static float speed = 25f;

    static  public IEnumerator IncreaseSpeed() {

        float elapsed = 0f;
        float duration = 10f;

        speed = 50f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        speed = 25f;
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
