using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimNShoot : MonoBehaviour
{

    private Camera cam;
    private Vector3 mousePos;
    private float shootingSpeed = 30f;
    private float maxAimAngle = 75f;
    [SerializeField] public Transform firePoint;

    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        aim();

        if (Input.GetButtonDown("Fire1"))
        {
            shoot(BulletType.PlayerBullet);
        }
        
    }

    public virtual void aim()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        angle = Mathf.Clamp(angle, -maxAimAngle, maxAimAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void shoot(BulletType type)
    {
        GameObject bullet = BulletPool.Instance.getBullets(type);

        if(bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * shootingSpeed, ForceMode2D.Impulse);
        }
    }
}
