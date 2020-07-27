using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Start_Game : MonoBehaviour
{
    public GameObject startCanvas_obj, gameCanvas_obj, menuCanvas_obj, howtoCanvas_obj;
    private void Start()
    {
        gameCanvas_obj = GameObject.Find("GameCanvas");
        startCanvas_obj = GameObject.Find("StartCanvas");
        menuCanvas_obj = GameObject.Find("MenuCanvas");
        howtoCanvas_obj = GameObject.Find("HowtoCanvas");

        gameCanvas_obj.SetActive(false);
        menuCanvas_obj.SetActive(false);
        howtoCanvas_obj.SetActive(false);
    }
    public void StartGameScene()
    {
        startCanvas_obj.SetActive(false);
        howtoCanvas_obj.SetActive(true);

        Invoke("RemoveHowto", 4);
    }

    public void RemoveHowto() {
        Drag_Object.wait = false;
        howtoCanvas_obj.SetActive(false);
        gameCanvas_obj.SetActive(true);
    }
}
