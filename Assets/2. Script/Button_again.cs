using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_again : MonoBehaviour
{
    public GameObject pongs_obj, menuCanvas_obj;
    public Text order_obj;

    private void Start()
    {
        menuCanvas_obj = GameObject.Find("MenuCanvas");
        pongs_obj = GameObject.Find("Pongs");
    }
    public void onClick_again() {
        Drag_Object.stack_ingredient = 0;
        Drag_Object.check_count = 0;
        Drag_Object.money = 0;
        Drag_Object.level = 0;
        Drag_Object.this_burger_price = 5000;
        Drag_Object.ingredient_y = (float)0.2;

        pongs_obj.GetComponent<Transform>().position = new Vector3(0, (float)2.5, (float)-5);
        for (int i = 0; i < 7; i++)
        {
            Destroy(Drag_Object.clone_ingredient[i]);
            Drag_Object.used_ingredient[i] = false;
            Drag_Object.fault_ingredient[i] = false;
            Drag_Object.clone_ingredient[i] = null;
        }
        order_obj.text = "아래 빵을 올리세요!";
        Drag_Object.wait = false;

        menuCanvas_obj.SetActive(false);
    }
}
