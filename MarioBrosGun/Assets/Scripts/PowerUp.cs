using UnityEngine;

public class PowerUp : MonoBehaviour
{
    AudioSource[] audioSources;
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
        fastShot
    }

    public Type type;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.CompareTag("Player")) {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;

            case Type.ExtraLife:
                Debug.Log("Suena 0 + " + audioSources.Length);
                audioSources[0].Play();
                GameManager.Instance.AddLife();
                break;

            case Type.MagicMushroom:
                
                player.GetComponent<Player>().Grow();
                
                break;

            case Type.Starpower:
                player.GetComponent<Player>().Starpower();
                break;
            case Type.fastShot:
                Bullet.IncreaseSpeedBullet();
                break;
        }


        Destroy(gameObject);
    }

}
