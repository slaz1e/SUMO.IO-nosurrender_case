using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float massIncreaseAmount = 1f; // K�tle art�� miktar�
    public float scaleIncreaseAmount = 0.1f; // �l�ek art�� miktar�
    private void OnTriggerEnter(Collider other)
    {
        // Etkile�im yapan nesne oyuncuysa
        if (other.CompareTag("Player")||other.CompareTag("Enemy"))
        {
            // K�tle art���
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
