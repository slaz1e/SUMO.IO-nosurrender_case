using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] public GameObject foodPrefab; // Food nesnesinin prefab'ý
    public float spawnDelay = 1f; // Yeni bir food nesnesi oluþturma gecikmesi
    public float spawnRadius = 5f; // Food nesnelerinin oluþma yarýçapý
    public float spawnHeight = 0.5f; // Food nesnelerinin oluþma yüksekliði

    private IEnumerator foodSpawnCoroutine; // Food nesnesi oluþturma coroutine'i

    private void Start()
    {
        // FoodSpawnCoroutine'i baþlat
        foodSpawnCoroutine = SpawnFoodCoroutine();
        StartCoroutine(foodSpawnCoroutine);
    }

    private IEnumerator SpawnFoodCoroutine()
    {
        while (true)
        {
            // Belirli bir süre bekle
            yield return new WaitForSeconds(spawnDelay);

            // Rastgele bir açý seç
            float angle = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(90f, angle, 90f);

            // Yatayda rastgele bir nokta seç
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPoint.x, spawnHeight, randomPoint.y) + transform.position;

            GameObject food = Instantiate(foodPrefab, spawnPosition, rotation);

            // Yemek oluþturulduðunda AI'larýn takip etmesini saðlamak için yemekleri AIController'a bildir
            EnemyAI [] aiControllers = FindObjectsOfType<EnemyAI>();
            foreach (EnemyAI aiController in aiControllers)
            {
                aiController.AddFood(food);
            }
        }
    }
}
