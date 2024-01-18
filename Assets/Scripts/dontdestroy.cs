using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroy : MonoBehaviour
{

    public void OnPress()
    {
        DontDestroyOnLoad(gameObject);
    }

}
