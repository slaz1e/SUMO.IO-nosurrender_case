using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClashScript : MonoBehaviour
{
    public float maxClashDistance = 15f; // Maksimum itme mesafesi
    public float clashForce = 15f; // �ttirme kuvveti
    [SerializeField] private Collider specialCollider;
    private void OnCollisionEnter(Collision collision)
    {
        // E�er �arp��an nesne bir oyuncuysa
        if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            // �ttirme mesafesini hesapla
            Vector3 clashDirection = collision.contacts[0].point - transform.position ;
            float clashDistance = clashDirection.magnitude;
            clashDirection = clashDirection.normalized;

            // �ttirme kuvvetini hesapla ve uygula (belirli bir mesafeden fazlaysa itme kuvveti s�f�r olacak)
            float clashForceToApply = Mathf.Lerp(clashForce, 0f, clashDistance / maxClashDistance);
            if (collision.contacts[0].thisCollider == specialCollider)
            {
                clashForceToApply +=100f;
            }
            rb.AddForce(clashDirection * clashForceToApply, ForceMode.Impulse);
            Debug.Log("de�di");
        }
    }
}
