using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_keepgoing : MonoBehaviour
{
    public GameObject menuCanvas_obj;
    // Start is called before the first frame update
    void Start()
    {
        menuCanvas_obj = GameObject.Find("MenuCanvas");

    }

    public void onClick_Keep_Going()
    {
        menuCanvas_obj.SetActive(false);
        Drag_Object.wait = false;
    }
}
