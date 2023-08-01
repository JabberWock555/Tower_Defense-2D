using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimNShoot : MonoBehaviour
{

    private Camera cam;
    private Vector3 mousePos;
    private float shootingSpeed = 30f;
    [SerializeField] private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, angle);

        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        
    }

    private void shoot()
    {
        GameObject bullet = BulletPool.Instance.getPlayerBullets();

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
