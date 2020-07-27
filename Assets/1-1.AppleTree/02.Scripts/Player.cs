using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera subCam;
    private GameObject player;
    private Rigidbody myRigidbody;
    public float screenX = 8.0f;
    public float screenY = 4.5f;

    private void Start()
    {
        player = this.gameObject;
        myRigidbody = GetComponentInChildren<Rigidbody>();
        subCam = GameObject.FindGameObjectWithTag("SubCam").GetComponent<Camera>();
    }
    
    private void Update()
    {
        Vector3 inputMousePoint = subCam.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition) - new Vector3(.5f, .5f, 0);

        Vector3 playerScreenPoint = new Vector3(inputMousePoint.x * 17.6f, 0, inputMousePoint.y * 9.9f);

        //보정
        if (playerScreenPoint.x <= -screenX)
        {
            playerScreenPoint = new Vector3(-screenX, playerScreenPoint.y, playerScreenPoint.z);
        }
        else if (playerScreenPoint.x >= screenX)
        {
            playerScreenPoint = new Vector3(screenX, playerScreenPoint.y, playerScreenPoint.z);
        }

        if (playerScreenPoint.z <= -screenY)
        {
            playerScreenPoint = new Vector3(playerScreenPoint.x, playerScreenPoint.y, -screenY);
        }
        else if (playerScreenPoint.z >= screenY)
        {
            playerScreenPoint = new Vector3(playerScreenPoint.x, playerScreenPoint.y, screenY);
        }

        player.transform.position = playerScreenPoint;
    }
}
