using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPointInfo
    {
        public Transform spawnPoint; // Punto de spawn para los enemigos
        public int maxEnemiesPerSpawnPoint = 3; // M�ximo de enemigos activos permitidos por punto de spawn
        public float spawnIntervalMin = 2f; // Intervalo m�nimo entre spawns de enemigos
        public float spawnIntervalMax = 5f; // Intervalo m�ximo entre spawns de enemigos

        [HideInInspector]
        public List<GameObject> activeEnemies = new List<GameObject>(); // Lista de enemigos activos en este punto de spawn
    }

    public GameObject enemyPrefab;
    public List<SpawnPointInfo> spawnPointsInfo; // Informaci�n sobre los puntos de spawn para los enemigos

    private void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            foreach (var spawnPointInfo in spawnPointsInfo)
            {
                if (spawnPointInfo.activeEnemies.Count < spawnPointInfo.maxEnemiesPerSpawnPoint)
                {
                    yield return new WaitForSeconds(Random.Range(spawnPointInfo.spawnIntervalMin, spawnPointInfo.spawnIntervalMax));
                    SpawnEnemy(spawnPointInfo);
                }
            }
            yield return null;
        }
    }

    void SpawnEnemy(SpawnPointInfo spawnPointInfo)
    {
        // Verificar si ya se alcanz� el m�ximo de enemigos por punto de spawn
        if (spawnPointInfo.activeEnemies.Count < spawnPointInfo.maxEnemiesPerSpawnPoint)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPointInfo.spawnPoint.position, spawnPointInfo.spawnPoint.rotation);
            spawnPointInfo.activeEnemies.Add(newEnemy);

            // Suscribirnos al evento OnDeath del nuevo enemigo y eliminarlo de la lista de enemigos activos cuando muera
            Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.OnDeath += () => { spawnPointInfo.activeEnemies.Remove(newEnemy); };
            }
        }
    }
}