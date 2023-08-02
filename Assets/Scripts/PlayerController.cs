using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

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
    [SerializeField] private int towerHealth;
    [SerializeField] private int playerHealth;

    public int getTowerHealth()
    {
        return towerHealth;
    }


    public int getPlayerHealth()
    {
        return playerHealth;
    }

    public void PlayerHealth(int _towerHealth, int _playerHealth)
    {
        towerHealth = _towerHealth;
        playerHealth = _playerHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if( towerHealth < 0 && playerHealth < 0)
        {
            GameManager.Instance.LevelFailed();
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            Damage(bullet.bulletDamage);
        }
    }

    public void Damage(int damage)
    {
        if (towerHealth > 0)
        {
            towerHealth -= damage;
        }
        else if (playerHealth > 0 && towerHealth <= 0)
        {
            playerHealth -= damage;
        }
    }

    
}
