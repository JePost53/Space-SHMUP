using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
     // Singleton
    static private Main S;

    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSec = 0.5f;
    public float enemyInsetDefault = 1.5f;

    public float gameRestartDelay = 2f;

    private InBoundsCheck boundCheck;
    private bool heroIsAlive = true;

    void Awake()
    {
        S = this;
        boundCheck = GetComponent<InBoundsCheck>();

        InvokeRepeating(nameof(SpawnEnemy), 2f, 1f / enemySpawnPerSec);
    }


    public void SpawnEnemy()
    {
        int index = UnityEngine.Random.Range(0, prefabEnemies.Length);
        GameObject enemyClone = Instantiate<GameObject>(prefabEnemies[index]);

        float enemyInset = enemyInsetDefault;
        if (enemyClone.GetComponent<InBoundsCheck>() != null)
        {
            enemyInset = Mathf.Abs(enemyClone.GetComponent<InBoundsCheck>().radius);
        }
        Vector3 pos = Vector3.zero;
        float xMin = -boundCheck.camWidth + enemyInset;
        float xMax = boundCheck.camWidth - enemyInset;

        pos.x = UnityEngine.Random.Range(xMin, xMax);
        pos.y = boundCheck.camHeight + enemyInset;
        enemyClone.transform.position = pos;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void HERO_DIED()
    {
        S.heroIsAlive = false;
        S.Invoke(nameof(Restart), S.gameRestartDelay); 
    }

    void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
