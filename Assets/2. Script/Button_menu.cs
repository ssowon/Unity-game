using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_menu : MonoBehaviour
{
    public GameObject startpanel_obj, gameCanvus_obj, menuCanvas_obj;
    public Text state_obj;
    // Start is called before the first frame update
    void Start()
    {
        gameCanvus_obj = GameObject.Find("GameCanvas");
        startpanel_obj = GameObject.Find("StartPanel");
        menuCanvas_obj = GameObject.Find("MenuCanvas");
    }

    public void onClick_menuButton() 
    {
        menuCanvas_obj.SetActive(true);
        state_obj.text = "일 시 정 지";
        Drag_Object.wait = true;
    }
}
