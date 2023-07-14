using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float massIncreaseAmount = 1f; // Kütle artýþ miktarý
    public float scaleIncreaseAmount = 0.1f; // Ölçek artýþ miktarý
    private void OnTriggerEnter(Collider other)
    {
        // Etkileþim yapan nesne oyuncuysa
        if (other.CompareTag("Player")||other.CompareTag("Enemy"))
        {
            // Kütle artýþý
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass += massIncreaseAmount;
            }

            Vector3 scaleIncrease = new Vector3(scaleIncreaseAmount, scaleIncreaseAmount, scaleIncreaseAmount);
            other.transform.localScale += scaleIncrease;
            Destroy(gameObject);
        }
    }
}
