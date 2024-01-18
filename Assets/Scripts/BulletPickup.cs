using System;
using System.Collections;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{

    public int BulletAdd = 10;
    //public GameObject ObjectToAccess;
    //public RandomSpawn m_otherScript;
    public GameObject PlayerCharacter;
    private void Start()
    {
        PlayerCharacter = GameObject.Find("PlayerCharacter");
    }

    void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")) return;

        BulletPowerUp(other);
    }

    private void BulletPowerUp(Collider collider)
    {

        //Debug.Log(collider);
        //GameObject CarControllGameObject = GameObject.Find("car");
        //CarController carController = CarControllGameObject.GetComponent<CarController>();
        //Collider carcontrol = collider;
        //carcontrol.GetComponent<CarController>().AmountOfBullets = 1000;
        if (PlayerCharacter == null)
        {
            collider.GetComponent<CarController>().AmountOfBullets += BulletAdd;
        }
        else
        {
            collider.GetComponent<GunShoot>().BulletAmount += BulletAdd;
        }
        Destroy(gameObject);
    }

    /* // Powerup Method //
    Multiplies Player Jump Strength by 2 for 8 seconds.
    Hides Powerup object for duration.
    Re-enables once duration is up.
    */



}
