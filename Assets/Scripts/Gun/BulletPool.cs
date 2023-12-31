using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private List<GameObject> playerBulletList;
    private List<GameObject> enemyBulletList;

    [SerializeField] private int playerBulletAmount;
    [SerializeField] private Bullet playerBullet;
    [SerializeField] private int enemyBulletAmount;
    [SerializeField] private Bullet enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        playerBulletList = new List<GameObject>();
        enemyBulletList = new List<GameObject>();

        for (int i = 0; i < playerBulletAmount; i++)
        {
            GameObject bullet = Instantiate<Bullet>(playerBullet, transform).gameObject;
            bullet.SetActive(false);
            playerBulletList.Add(bullet);
        }

        for (int i = 0; i < enemyBulletAmount; i++)
        {
            GameObject e_bullet = Instantiate<Bullet>(enemyBullet, transform).gameObject;
            e_bullet.SetActive(false);
            enemyBulletList.Add(e_bullet);
        }
    }

    public GameObject getBullets(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.PlayerBullet:
                for (int i = 0; i < playerBulletList.Count; i++)
                {
                    if (!playerBulletList[i].activeInHierarchy)
                    {
                        return playerBulletList[i];
                    }
                }
                break;

            case BulletType.EnemyBullet:
                for (int i = 0; i < enemyBulletList.Count; i++)
                {
                    if (!enemyBulletList[i].activeInHierarchy)
                    {
                        return enemyBulletList[i];
                    }
                }
                break;
        }
        return null;
    }

    private void OnDisable()
    {
        if (enemyBulletList != null)
        {
            for (int i = 0; i < enemyBulletList.Count; i++)
            {
                if (enemyBulletList[i] != null && enemyBulletList[i].activeInHierarchy)
                {
                    enemyBulletList[i].SetActive(false);
                }
            }
        }

        if (playerBulletList != null)
        {
            for (int i = 0; i < playerBulletList.Count; i++)
            {
                if (playerBulletList[i] != null && playerBulletList[i].activeInHierarchy)
                {
                    playerBulletList[i].SetActive(false);
                }
            }
        }
    }
}

