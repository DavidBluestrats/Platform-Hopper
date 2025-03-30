using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float secondsBetweenSpawns = 0.4f;
    float nextSpawnTime;
    [SerializeField]
    private GameObject platformPrefab;
    private GameObject[] platforms = new GameObject[1000];
    private int mode = 0;

    [SerializeField]
    private GameObject enemy;
    public int enemyNum;

    private int score = 0;
    private Vector3 scorePos;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject gameOverGUI;


    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        StartGame(); 
    }

    public void StartGame()
    {
        mode = 0;
        Vector3 pos = new Vector3();
        scorePos = new Vector3();

        for (int i = 0; i < 7; i++)
        {
            pos.y += Random.Range(0.6f, 1.7f);
            pos.x = Random.Range(-6f, 6f);
            Instantiate(platformPrefab, pos, Quaternion.identity);
        }

        float size = pos.y / enemyNum;
        pos = new Vector3();

        for (int i = 0; i < enemyNum; i++)
        {
            pos.y = Random.Range(size * i, size * (i + 1));
            pos.x = Random.Range(-6f, 6f);
            Instantiate(enemy, pos, Quaternion.identity);
        }

        gameOverGUI.SetActive(false);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        Player.transform.position = new Vector3(-0.1232511f, 1.09f, 0f);
        Player.GetComponent<BoxCollider2D>().enabled = true;
        camera.transform.position = new Vector3(0f, 0f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            if (Player.transform.position.y > scorePos.y)
            {
                scorePos = Player.transform.position;
                score = (int)(Player.transform.position.y * 15.0f);
                scoreText.text = "Score: " + score;
            }
        }

        if (Player.name == "GameOver")
        {
            gameOverGUI.SetActive(true);
            mode = 1;
            Player.name = "Player";
            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] platformArray = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject enemy in enemyArray)
            {
                Destroy(enemy);
            }
            foreach (GameObject platform in platformArray)
            {
                Destroy(platform);
            }



        }
        if (Player.transform.position.y > camera.transform.position.y)
        {
            if (Time.time > nextSpawnTime)
            {
                Vector3 pos = new Vector3();
                Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
                nextSpawnTime = Time.time + secondsBetweenSpawns;
                float halfHeight = Camera.main.orthographicSize;
                float halfWidth = Camera.main.aspect * halfHeight;
                pos.x = Random.Range(-halfWidth, halfWidth);
                pos.y = (stageDimensions.y) + 2.2f;
                Instantiate(platformPrefab, pos, Quaternion.identity);
                if (rollForEnemy())
                {
                    pos.x = Random.Range(-halfWidth, halfWidth);
                    pos.y = (stageDimensions.y) + 2.2f;
                    Instantiate(enemy, pos, Quaternion.identity);
                }
            }
           
        }
    }
    private bool rollForEnemy()
    {
        return Random.Range(1, 10) == 1 ? true : false;
    }

}
