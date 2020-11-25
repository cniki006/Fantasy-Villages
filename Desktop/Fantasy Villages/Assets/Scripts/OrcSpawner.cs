using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    public int waveSize;
    float waveTimer;
    public float timerCount;
    // Start is called before the first frame update
    void Start()
    {
        waveTimer = timerCount;
    }

    // Update is called once per frame
    void Update()
    {

        timerCount -= Time.deltaTime;
        if (timerCount <= 0 && GameObject.Find("GameManager").GetComponent<GameManager_Script>().allEnemies.Length <= 50)
        {

            spawnWave(); 
            waveSize += 2;
            timerCount = waveTimer / 2;
        }
    }

    void spawnWave()
    {
        for (int i = 0; i < waveSize; i++)
        {
            Instantiate(enemy1, new Vector3(transform.position.x + Random.Range(-100, 100), 0, transform.position.z + Random.Range(-100, 100)), Quaternion.identity);
        }
    }


}
