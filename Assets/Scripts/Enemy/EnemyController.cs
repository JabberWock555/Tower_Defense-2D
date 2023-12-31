using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GunController
{
    [SerializeField] private float shootingTime;
    [SerializeField] private float movingSpeed;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int health;

    private float moveLimit = 10f;
    private float timer;
    private Vector3 direction;
    private readonly float aimOffset = 180f;

    private void Start()
    { 
        timer = (float)(Random.Range(0, 10) * 0.1);
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (health > 0 && GameManager.Instance.Playing)
        {
            Aim();
            movement();

            timer += Time.deltaTime;
            if (timer > shootingTime)
            {
                timer = 0;
                Shoot(BulletType.EnemyBullet);
            }
        }
        else
        {
            EnemySpawner.Instance.removeEnemy(gameObject);
            Destroy(gameObject);
            SoundManager.Instance.Play(SoundEvents.EnemyDestroy);
        }
    }

    public void Damage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
    }

    public override void Aim()
    {
        direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        firePoint.rotation = Quaternion.Euler( 0f, 0f, angle + aimOffset);
    }
    

    private void movement()
    {
        if (direction.magnitude >= moveLimit)
        {
            transform.position += direction.normalized * movingSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            Damage(bullet.bulletDamage);

            GameManager.Instance.PlayerHit();
        }
    }

}
