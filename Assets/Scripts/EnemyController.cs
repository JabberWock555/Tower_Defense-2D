using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AimNShoot
{
    [SerializeField] private float shootingTime;
    [SerializeField] private float movingSpeed;
    [SerializeField] private Transform playerTransform;

    private float timer;
    private Vector3 direction;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        aim();
        movement();

        timer += Time.deltaTime;
        if(timer > shootingTime)
        {
            timer = 0;
            Debug.Log("Enemy Fire");
            shoot(BulletType.EnemyBullet);
        }
    }

    public override void aim()
    {
        direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        firePoint.rotation = Quaternion.Euler( 0f, 0f, angle + 180f);
    }
    

    private void movement()
    {
        transform.position += direction.normalized * movingSpeed * Time.deltaTime;
    }

}
