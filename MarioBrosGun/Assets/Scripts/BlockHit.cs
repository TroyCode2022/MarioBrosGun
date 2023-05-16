using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = 1;
    private bool animating; 
    public GameObject bulletPrefab; //TODO: Que no sea una variable publica
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up)) {
                if (this.gameObject.CompareTag("Brick"))
                    HitBrick(collision.gameObject.GetComponent<Player>().big);
                else
                    hitLucky();
                
            }
        }
    }

    public void HitBrick(bool isBig)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.enabled = true; // show if hidden

        if (maxHits != 0)
        {

            if (isBig)
            {
                convertToEmpty(ref spriteRenderer);
            }

            StartAnimation();
        }

    }

    private void StartAnimation()
    {
        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    public void hitLucky()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // show if hidden

        if (maxHits != 0)
        {

            convertToEmpty(ref spriteRenderer);
            Debug.Log(maxHits);

            StartAnimation();
        }

    }

    private void convertToEmpty(ref SpriteRenderer spriteRenderer)
    {
        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

    }

    //private void Hit()
    //{
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.enabled = true; // show if hidden

    //    maxHits--;

    //    if (maxHits == 0) {
    //        spriteRenderer.sprite = emptyBlock;
    //    }

    //    if (item != null) {
    //        Instantiate(item, transform.position, Quaternion.identity);
    //    }

    //    StartCoroutine(Animate());
    //}

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
    }

