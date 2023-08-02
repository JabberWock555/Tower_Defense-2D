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
    [SerializeField] private int playerBulletAmount;
    [SerializeField] private Bullet playerBullet;

    // Start is called before the first frame update
    void Start()
    {
        playerBulletList = new List<GameObject>();

        for(int i =0; i<playerBulletAmount; i++)
        {
            GameObject bullet = Instantiate<Bullet>(playerBullet).gameObject;
            bullet.SetActive(false);
            playerBulletList.Add(bullet);
        }
        
    }

    public GameObject getPlayerBullets()
    {
        for( int i = 0; i < playerBulletList.Count; i++)
        {
            if (!playerBulletList[i].activeInHierarchy)
            {
                return playerBulletList[i];
            }
        }
        return null;
    }
}
