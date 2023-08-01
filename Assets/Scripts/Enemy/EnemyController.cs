using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GunController
{
    [SerializeField] private float shootingTime;
    [SerializeField] private float movingSpeed;
    [SerializeField] private Transform playerTransform;

    private int health;
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
        if (health != 0)
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
            Destroy(gameObject);
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
        transform.position += direction.normalized * movingSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            Damage(bullet.bulletDamage);
        }
    }
}
