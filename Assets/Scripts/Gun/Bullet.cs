using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletType bulletType;
    public int bulletDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Instance.Play(SoundEvents.BulletHit);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!GameManager.Instance.Playing)
        {
            gameObject.SetActive(false);
        }
    }

}

public enum BulletType
{
    PlayerBullet,
    EnemyBullet
};