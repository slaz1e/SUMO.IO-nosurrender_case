using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] public GameObject foodPrefab; // Food nesnesinin prefab'�
    public float spawnDelay = 1f; // Yeni bir food nesnesi olu�turma gecikmesi
    public float spawnRadius = 5f; // Food nesnelerinin olu�ma yar��ap�
    public float spawnHeight = 0.5f; // Food nesnelerinin olu�ma y�ksekli�i

    private IEnumerator foodSpawnCoroutine; // Food nesnesi olu�turma coroutine'i

    private void Start()
    {
        // FoodSpawnCoroutine'i ba�lat
        foodSpawnCoroutine = SpawnFoodCoroutine();
        StartCoroutine(foodSpawnCoroutine);
    }

    private IEnumerator SpawnFoodCoroutine()
    {
        while (true)
        {
            // Belirli bir s�re bekle
            yield return new WaitForSeconds(spawnDelay);

            // Rastgele bir a�� se�
            float angle = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(90f, angle, 90f);

            // Yatayda rastgele bir nokta se�
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPoint.x, spawnHeight, randomPoint.y) + transform.position;

            GameObject food = Instantiate(foodPrefab, spawnPosition, rotation);

            // Yemek olu�turuldu�unda AI'lar�n takip etmesini sa�lamak i�in yemekleri AIController'a bildir
            EnemyAI [] aiControllers = FindObjectsOfType<EnemyAI>();
            foreach (EnemyAI aiController in aiControllers)
            {
                aiController.AddFood(food);
            }
        }
    }
}
