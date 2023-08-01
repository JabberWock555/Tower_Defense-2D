using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;
    private float shootingSpeed = 30f;
    private float maxAimAngle = 75f;
    protected float rotationOffset = 90f;

    [SerializeField] public Transform firePoint;

    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        Aim();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(BulletType.PlayerBullet);
        }

    }



    public virtual void Aim()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationOffset;
        angle = Mathf.Clamp(angle, -maxAimAngle, maxAimAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Shoot(BulletType type)
    {
        GameObject bullet = BulletPool.Instance.getBullets(type);

        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * shootingSpeed, ForceMode2D.Impulse);
        }
    }
}
