using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemyCount;
    public float spawnTime;
    
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;
    private List<GameObject> enemyList;
    private Transform spawnPoint;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
        spawnPoint = new GameObject().transform;
        spawnPoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count <= enemyCount)
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                timer = 0;
                spwanEnemy();
            }
        }
        
    }

    private void changeLocation()
    {
        float location = Random.Range(leftBound.position.x, rightBound.position.x);
        spawnPoint.position = new Vector2(location, transform.position.y);
    }

    private void spwanEnemy()
    {
        changeLocation();
        GameObject enemy = Instantiate<EnemyController>(enemyPrefab, spawnPoint.position, transform.rotation, transform).gameObject;
        enemyList.Add(enemy);
    }
}
