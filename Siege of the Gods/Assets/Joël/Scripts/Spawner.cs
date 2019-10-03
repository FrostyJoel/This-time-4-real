using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawnLocation;
    private bool spawning;
    public bool startSpawning;
    public int waveAmount;
    public int wave;
    public float time;
    public float roundtime;
    public float spawnTime;

    public UIManager ui;

    private void Awake()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        time = roundtime;
    }

    public void StartSpawn()
    {
        time = roundtime;
        waveAmount += wave;
        wave++;
        spawning = true;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int amount = 0;
        while (spawning && UIManager.gameSpeed > 0)
        {
            amount++;
            Instantiate(enemy, spawnLocation.position, enemy.transform.rotation);
            if(amount >= waveAmount)
            {
                yield break;
            }
            yield return new WaitForSeconds(spawnTime / UIManager.gameSpeed);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if(UIManager.gameSpeed > 0 && time > 0)
        {
            time -= Time.deltaTime * UIManager.gameSpeed;
            if (startSpawning || time <= 0)
            {
                StartSpawn();
            }
            if (startSpawning)
            {
                startSpawning = false;
            }
        }
        ui.totalTimeLeft = Mathf.RoundToInt(time);
        
    }
}
