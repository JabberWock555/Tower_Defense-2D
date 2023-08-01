using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletType bulletType;
    public int bulletDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

}

public enum BulletType
{
    PlayerBullet,
    EnemyBullet
};