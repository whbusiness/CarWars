using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    public new Transform camera;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(90, camera.eulerAngles.y, 0);
    }

}
