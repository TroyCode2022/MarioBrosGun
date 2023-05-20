using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.Playables;

public class SpecialAnimation : MonoBehaviour
{

    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int frame;
    private Rigidbody2D rb;

    private Vector2 currentPosition;
    float elevation = 0.6f;


    private void OnEnable()
    {
        frame = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            GetComponent<HitAnimation>().enabled = true;
        }
        

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];

            if(frame == 3)
            {
                

                // Calcula la nueva posición elevada sumando un desplazamiento en el eje Y
                 // Ajusta este valor según la altura deseada
                Vector2 newPosition = currentPosition + new Vector2(0f, elevation);

                // Mueve el objeto a la nueva posición
                rb.MovePosition(newPosition);
            }
        }
    }
}
