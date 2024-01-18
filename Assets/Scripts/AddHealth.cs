using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    public int AddToHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        else
        {
            CarController.health += AddToHealth;
            Destroy(gameObject);
            PickupSpawner.MaxInScene -= 1;
        }
    }
}
