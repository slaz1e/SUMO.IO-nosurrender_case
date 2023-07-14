using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float navMeshDisableDistance = 9f; // NavMeshAgent'in kapatýlacaðý mesafe
    public float followDistance = 5f; // Takip etme mesafesi
    [SerializeField] private Transform player, ground; // Oyuncu
    private NavMeshAgent agent; // Yol bulma ajaný
    private UI ui;
    private List<GameObject> foods; // Yemeklerin listesi

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncuyu bul
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent bileþenini al
        ui = FindObjectOfType<UI>();
        // Yemekleri bul ve listeye ekle
        foods = new List<GameObject>(GameObject.FindGameObjectsWithTag("Food"));
    }

    private void Update()
    {
        if (player != null && player.gameObject.activeSelf)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, ground.position);

            // Oyuncu takip mesafesindeyse
            if (distanceToPlayer < navMeshDisableDistance && distanceToPlayer > -navMeshDisableDistance)
            {
                // NavMeshAgent'i aç
                agent.enabled = true;
            }
            else
            {
                agent.enabled = false;
            }

            // En yakýn yemeðin konumunu bul
            Vector3 closestFoodPosition = FindClosestFood();

            // Yemeðe takip mesafesindeyse
            if (Vector3.Distance(transform.position, closestFoodPosition) <= followDistance)
            {
                // Yemeðin konumuna doðru hareket et
                agent.SetDestination(closestFoodPosition);
            }
            else
            {
                // Oyuncunun konumuna doðru hareket et
                if (agent.enabled)
                    agent.SetDestination(player.position);
            }
        }
    }
    public void AddFood(GameObject food)
    {
        foods.Add(food);
    }
    private Vector3 FindClosestFood()
    {
        Vector3 closestFoodPosition = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        // Tüm yemekleri dön ve en yakýn yemeði bul
        foreach (GameObject food in foods)
        {
            if (food != null)
            {
                float distance = Vector3.Distance(transform.position, food.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestFoodPosition = food.transform.position;
                }
            }
        }

        return closestFoodPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            // Yemek yendiðinde listeden çýkar
            GameObject food = other.gameObject;
            foods.Remove(food);
            
        }
        else if (other.CompareTag("Water"))
        {
            ui.RemoveEnemy(this.gameObject);
            Destroy(gameObject);
        }
    }
}
