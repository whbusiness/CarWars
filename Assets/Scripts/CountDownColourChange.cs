using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownColourChange : MonoBehaviour
{
    private Renderer boxrenderer;
    // Start is called before the first frame update
    void Start()
    {
        boxrenderer = GetComponent<Renderer>();
        ChangeColourRed();
    }

    void ChangeColourRed()
    {
        boxrenderer.material.color = Color.red;
        Invoke("ChangeColourAmber", 1.3f);
    }

    void ChangeColourAmber()
    {
        boxrenderer.material.color = Color.yellow;
        Invoke("ChangeColourGreen", 1.2f);
    }

    void ChangeColourGreen()
    {
        boxrenderer.material.color = Color.green;
        Invoke("Hide", 0.4f);
    }

    private void Hide()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }


}
