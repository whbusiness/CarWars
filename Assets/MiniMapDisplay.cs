using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] MiniMapObjects;
    public GameObject player;
    private void Start()
    {
        player = GameObject.Find("PlayerCharacter");
    }
    void Update()
    {
        //Arrow.transform.rotation = camera.transform.rotation;
        if (player == null)
        {
            MiniMapObjects[0].SetActive(false);
            MiniMapObjects[1].SetActive(false);
            MiniMapObjects[2].SetActive(true);
            MiniMapObjects[3].SetActive(true);
        }
        else
        {
            MiniMapObjects[0].SetActive(true);
            MiniMapObjects[1].SetActive(true);
            MiniMapObjects[2].SetActive(false);
            MiniMapObjects[3].SetActive(false);
        }
    }
}
