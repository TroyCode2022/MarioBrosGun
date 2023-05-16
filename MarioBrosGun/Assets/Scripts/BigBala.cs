using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBala : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Player player = collision.gameObject.GetComponent<Player>();
    //        Debug.Log("Bala tocado saltando");
    //        if (collision.transform.DotTest(transform, Vector2.down))
    //        {
    //            Hit();
    //        }
    //        else
    //        {
    //            player.Hit();
    //        }
    //    }

    //}

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }


}
