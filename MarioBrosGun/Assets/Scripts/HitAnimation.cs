using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class HitAnimation : MonoBehaviour
{
    public Sprite Sprite;
    public Sprite HitSprite;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = Sprite;
    }


    public IEnumerator doAnimation()
    {
        spriteRenderer.sprite = HitSprite;
        yield return new WaitForSeconds(0.18f);
        spriteRenderer.sprite = Sprite;
    }

    //private void Animate()
    //{
    //    frame++;

    //    if (frame >= sprites.Length)
    //    {
    //        frame = 0;
    //    }

    //    if (frame >= 0 && frame < sprites.Length)
    //    {
    //        spriteRenderer.sprite = sprites[frame];
    //    }
    //}

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
