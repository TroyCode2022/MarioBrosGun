using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.Playables;

public class ExplosionAnimation : MonoBehaviour
{

    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int frame;
    private Rigidbody2D rb;

    private Vector2 currentPosition;


    private void OnEnable()
    {
        frame = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Collider2D>().enabled = false;
        InvokeRepeating(nameof(Animate), framerate, framerate);
        rb = GetComponent<Rigidbody2D>();
        currentPosition = rb.position;

    }

    private void OnDisable()
    {
        CancelInvoke();

    }

    private void Animate()
    {
        frame++;

        if (frame >= sprites.Length)
        {

            this.enabled = false;
            Destroy(gameObject);
        }


        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];

         
        }
    }
}
