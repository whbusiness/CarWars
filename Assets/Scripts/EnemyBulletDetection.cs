using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBulletDetection : MonoBehaviour
{
    private GameObject playerCharacter;
    PlayerController FindHealthOverlay;
    CarController FindHealthOverlayCar;
    public static float timeOfHit;
    private void Start()
    {
        playerCharacter = GameObject.Find("PlayerCharacter");
        FindHealthOverlay = FindObjectOfType<PlayerController>();
        FindHealthOverlayCar = FindObjectOfType<CarController>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if(playerCharacter != null)
            {
                timeOfHit = Time.time;
                FindHealthOverlay.DMGOverlay();
            }
            else if(playerCharacter == null)
            {
                timeOfHit = Time.time;
                FindHealthOverlayCar.DMGOverlay();
            }
            //Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy2"))
        {
            Destroy(gameObject);
        }
    }
    /*IEnumerator DelayRumble()
    {
        //Gamepad.current.SetMotorSpeeds(0f, 0f);
        Debug.Log("running delay and turn rumble ON");
        yield return new WaitForSeconds(3f);
        //yield return null;
        Debug.Log("Reset Haptics");
        Gamepad.current.SetMotorSpeeds(0f, 0f); 
        Gamepad.current.ResetHaptics();
        yield return new WaitForSeconds(1.5f);
         

    }*/
}