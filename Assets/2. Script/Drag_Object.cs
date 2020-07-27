using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Drag_Object : MonoBehaviour
{
    public GameObject cucumber_obj, bottom_obj, patty_obj, tomato_obj, top_obj, cheese_obj, cup_obj, 
        menuCanvas_obj, firstReceipt_obj, secondRecipt_obj, thirdRecipt_obj, keepgoingButton_obj;
    public GameObject check_obj;
    public Text order_obj, money_obj, state_obj;

    float distance = 10;
    public Vector3 hand;                    //집게 위치

    public static float stack_ingredient = 0;      //쌓인 개수

    public Vector3 check_v;                 //커서

    public static float check_count = 0;           //원 크기
    public int last_check = 0;              //최근 쌓은 재료
    public static int money = 0;                   //돈
    public static int level = 0;                   //만든 개수
    public static int this_burger_price = 5000;           //버거 가격 깎기

    public int[] first_order = { 0, 1, 3, 5 };
    public int[] second_order = { 0, 1, 2, 3, 4, 5, 6 };
    public int[] third_order = { 0, 2, 3, 4, 5, 6 };
    string[] ingredient = { "아래 빵", "피클", "패티", "토마토", "치즈", "윗 빵", "콜라" };
    public static bool[] used_ingredient = { false, false, false, false, false, false, false };
    public static bool[] fault_ingredient = { false, false, false, false, false, false, false };
    public static GameObject[] clone_ingredient = { null, null, null, null, null, null, null };
    public static float ingredient_y = (float)0.2;
    public bool goto_ingredient = true;

    public static bool wait = true;

    void Start()
    {
        bottom_obj = GameObject.Find("Bread_bottom");
        patty_obj = GameObject.Find("Patty");
        top_obj = GameObject.Find("Bread_top");
        cucumber_obj = GameObject.Find("Cucumber");
        tomato_obj = GameObject.Find("Tomato");
        cheese_obj = GameObject.Find("Cheese");
        cup_obj = GameObject.Find("CoffeeCupwithcofee");
        check_obj = GameObject.Find("Check");
        menuCanvas_obj = GameObject.Find("MenuCanvas");
        firstReceipt_obj = GameObject.Find("First_Receipt");
        secondRecipt_obj = GameObject.Find("Second_Receipt");
        thirdRecipt_obj = GameObject.Find("Third_Receipt");
        keepgoingButton_obj = GameObject.Find("KeepGoingButton");
    }
    private void OnMouseDown()
    {
        goto_ingredient = true;
    }
    void OnMouseDrag()
    {
        if (!wait && goto_ingredient)
        {
            print("Drag!!");
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(objPosition.x, (float)2.5, (float)objPosition.z);
        }
    }

    void Update()
    {
        hand = transform.position;
        if (!wait && goto_ingredient)
        {
            if (-6 < hand.x && hand.x < -3 && 1 < hand.z && hand.z < 4 && !used_ingredient[0]) { if (Wait_Make_Burger(0, bottom_obj)) used_ingredient[0] = true; }
            else if (-7 < hand.x && hand.x < -4 && -6 < hand.z && hand.z < -2.8 && !used_ingredient[1]) { if (Wait_Make_Burger(1, cucumber_obj)) used_ingredient[1] = true; }
            else if (-8 < hand.x && hand.x < -4 && -2 < hand.z && hand.z < 1 && !used_ingredient[2]) { if (Wait_Make_Burger(2, patty_obj)) used_ingredient[2] = true; }
            else if (3 < hand.x && hand.x < 6 && 1 < hand.z && hand.z < 4 && !used_ingredient[3]) { if (Wait_Make_Burger(3, tomato_obj)) used_ingredient[3] = true; }
            else if (4 < hand.x && hand.x < 7 && -6 < hand.z && hand.z < -2.8 && !used_ingredient[4]) { if (Wait_Make_Burger(4, cheese_obj)) used_ingredient[4] = true; }
            else if (4 < hand.x && hand.x < 8 && -2 < hand.z && hand.z < 1 && !used_ingredient[5]) { if (Wait_Make_Burger(5, top_obj)) used_ingredient[5] = true; }
            else if (-2 < hand.x && hand.x < 2 && -1 < hand.z && hand.z < 4 && !used_ingredient[6]) { if (Wait_Make_Burger(6, cup_obj)) used_ingredient[6] = true; }
            else
            {
                last_check = -1;
                check_count = 0;
                check_obj.GetComponent<Transform>().position = new Vector3(100, 100, 100);
            }
        }
        switch (level)
        {
            case 0:
                Make_Order(first_order);
                break;
            case 1:
                Make_Order(second_order);
                break;
            case 2:
                Make_Order(third_order);
                break;
        }
        money_obj.text = "매출 : " + money.ToString() + " \n햄버거 가격 : " + this_burger_price.ToString();
    }

    //주문 진행
    public void Make_Order(int[] order_ingredient)
    {
        if (stack_ingredient == 0) { if (wait) { Invoke("Reset_Order", 2); } else Print_Order(order_ingredient[0]); }
        else if (stack_ingredient == order_ingredient.Length)
        {
            wait = true;
            money += this_burger_price;
            switch (this_burger_price)
            {
                case 5000:
                    order_obj.text = "Perfect!";
                    break;
                case 4000:
                    order_obj.text = "Great!";
                    break;
                default:
                    order_obj.text = "So so!";
                    break;
            }
            //초기화
            stack_ingredient = 0;
            this_burger_price = 5000;
            ingredient_y = (float)0.2;
            switch (level)
            {
                case 0:
                    firstReceipt_obj.SetActive(false);
                    level++;
                    break;
                case 1:
                    secondRecipt_obj.SetActive(false);
                    level++;
                    break;
                case 2:
                    menuCanvas_obj.SetActive(true);
                    keepgoingButton_obj.SetActive(false);
                    state_obj.text = "성 공 !";
                    break;
            }
            transform.position = new Vector3(0, (float)2.5, (float)-5);
        }
        else if (stack_ingredient > 0 && used_ingredient[order_ingredient[(int)stack_ingredient - 1]]) 
        {
            Print_Order(order_ingredient[(int)stack_ingredient]);
        }
    }

    //check obj 만들기
    public bool Wait_Make_Burger(int Tag_Check, GameObject item_obj)
    {
        if (last_check == Tag_Check && check_count < 20)
        {
            check_count += (float)0.3;
            check_obj.GetComponent<Transform>().localScale = new Vector3(check_count, check_count, check_count);
        }
        else if (last_check == Tag_Check && 20 <= check_count)
        {
            check_count = 0;
            last_check = -1;
            check_obj.GetComponent<Transform>().position = new Vector3(100, 100, 100);
            goto_ingredient = false;
            transform.position = new Vector3(0, (float)2.5, -5);

            switch (level)
            {
                case 0:
                    if (check_order(Tag_Check, item_obj, first_order)) return true;
                    else
                    {
                        if (!fault_ingredient[Tag_Check] && this_burger_price >= 3000) this_burger_price -= 1000;
                        fault_ingredient[Tag_Check] = true;
                        return false;
                    }
                case 1:
                    if (check_order(Tag_Check, item_obj, second_order)) return true;
                    else
                    {
                        if (!fault_ingredient[Tag_Check] && this_burger_price >= 3000) this_burger_price -= 1000;
                        fault_ingredient[Tag_Check] = true;
                        return false;
                    }
                case 2:
                    if (check_order(Tag_Check, item_obj, third_order)) return true;
                    else
                    {
                        if (!fault_ingredient[Tag_Check] && this_burger_price >= 3000) this_burger_price -= 1000;
                        fault_ingredient[Tag_Check] = true;
                        return false;
                    }
            }
        }
        else
        {
            check_v = item_obj.GetComponent<Transform>().position;
            //change check circle's position
            if (Tag_Check == 0) check_obj.GetComponent<Transform>().position = new Vector3(check_v.x + (float)0.7, check_v.y + (float)1.5, check_v.z - (float)1.2);
            else if (Tag_Check == 1) check_obj.GetComponent<Transform>().position = new Vector3(check_v.x + (float)0.4, check_v.y + (float)1.5, check_v.z - (float)0.7);
            else if (Tag_Check == 2) check_obj.GetComponent<Transform>().position = new Vector3(check_v.x + (float)1.1, check_v.y + (float)1.5, check_v.z - (float)0.4);
            else if (Tag_Check == 3) check_obj.GetComponent<Transform>().position = new Vector3(check_v.x - (float)0.4, check_v.y + (float)1.5, check_v.z - (float)1.0);
            else if (Tag_Check == 4) check_obj.GetComponent<Transform>().position = new Vector3(check_v.x - (float)0.7, check_v.y + (float)1.5, check_v.z - (float)0.2);
            else if (Tag_Check == 5) check_obj.GetComponent<Transform>().position = new Vector3(check_v.x - (float)0.6, check_v.y + (float)1.5, check_v.z - (float)1.2);
            else if (Tag_Check == 6) check_obj.GetComponent<Transform>().position = new Vector3(check_v.x, check_v.y + (float)1.5, check_v.z - (float)0.8);
            check_obj.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

            check_count = 0;
            last_check = Tag_Check;
        }
        return false;
    }

    //쟁반에 재료 생성
    public void Make_Burger(GameObject item_obj, int Tag_Check)
    {
        switch (Tag_Check)
        {
            case 0:
                clone_ingredient[(int)stack_ingredient] = Instantiate(item_obj, new Vector3(1, (float)0.2, -4), Quaternion.identity);
                ingredient_y += (float)0.2;
                break;
            case 1:
                clone_ingredient[(int)stack_ingredient] = Instantiate(item_obj, new Vector3((float)1.1, ingredient_y - (float)0.3, (float)-3.8), Quaternion.identity);
                ingredient_y += (float)0.08;
                break;
            case 2:
                clone_ingredient[(int)stack_ingredient] = Instantiate(item_obj, new Vector3(1, ingredient_y, -4), Quaternion.identity);
                ingredient_y += (float)0.15;
                break;
            case 3:
                clone_ingredient[(int)stack_ingredient] = Instantiate(item_obj, new Vector3((float)0.8, ingredient_y + (float)0.15, -4), Quaternion.identity);
                ingredient_y += (float)0.08;
                break;
            case 4:
                clone_ingredient[(int)stack_ingredient] = Instantiate(item_obj, new Vector3(1, ingredient_y, -4), Quaternion.identity);
                ingredient_y += (float)0.14;
                break;
            case 5:
                clone_ingredient[(int)stack_ingredient] = Instantiate(item_obj, new Vector3(1, ingredient_y - (float)0.2, -4), Quaternion.identity);
                break;
        }
    }
    //재료 obj 파괴
    public void Destroy_Burger(GameObject item_obj)
    {
        Destroy(item_obj);
    }
    //콜라 obj 생성
    public void Make_Coke(GameObject item_obj)
    {
        clone_ingredient[6] = Instantiate(item_obj, new Vector3(-1, 0, (float)-4), Quaternion.identity);
    }

    public void Print_Order(int order)
    {
        if (order == 0 || order == 1 || order == 5) order_obj.text = ingredient[order] + "을 올리세요!";
        else order_obj.text = ingredient[order] + "를 올리세요!";
    }

    // print order bottom & reset ingredient
    public void Reset_Order()
    {
        stack_ingredient = 0;
        this_burger_price = 5000;
        ingredient_y = (float)0.2;
        for (int i = 0; i < 7; i++)
        {
            Destroy(clone_ingredient[i]);
            used_ingredient[i] = false;
            fault_ingredient[i] = false;
            clone_ingredient[i] = null;
        }
        order_obj.text = "아래 빵을 올리세요!";
        wait = false;
    }

    //올린 재료 맞는지 확인
    public bool check_order(int Tag_Check, GameObject item_obj, int[] order_array)
    {

        if (Tag_Check == order_array[(int)stack_ingredient])
        {
            if (Tag_Check == 6) Make_Coke(item_obj); else Make_Burger(item_obj, Tag_Check);
            stack_ingredient++;

            return true;
        }
        return false;
    }

    public void Wrong_Ingredient()
    {
        if (money <= 2000) order_obj.text = "틀렸습니다!";
        else order_obj.text = "틀렸습니다! -1000";
    }
}