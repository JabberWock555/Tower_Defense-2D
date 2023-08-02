using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance { get { return instance; } }

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

    public int enemyCount;
    public float spawnTime;
    public int enemyLeft;
    
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    [SerializeField] private List<GameObject> enemyList;
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
        if (enemyCount > 0 && GameManager.Instance.Playing)
        {
            timer += Time.deltaTime;
            if (timer > spawnTime)
            {
                timer = 0;
                spwanEnemy();
            }
        }

        if(enemyCount <= 0)
        {
            GameManager.Instance.LevelComplete();
            GameManager.Instance.Playing = false;
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

    public void removeEnemy(GameObject enemy)
    {
        enemyList.RemoveAt(enemyList.IndexOf(enemy));
        enemyCount--;
    }

    public void setEnemyCount(int _enemyCount)
    {
        enemyCount = _enemyCount;
    }
}
