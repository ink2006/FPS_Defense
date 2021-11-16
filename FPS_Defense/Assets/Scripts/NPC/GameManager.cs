using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool canPlayerMove = true;

    public static bool isOpenIventory = false;
    public static bool isOpenCraftManual = false;

    public static bool isNight = false;
    public static bool isWater = false;

    // Update is called once per frame
    void Update()
    {
        if (isOpenIventory || isOpenCraftManual)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;  
            canPlayerMove = false;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canPlayerMove = true;
        }
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
