using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName;
    public GameObject go_Prefab;
    public GameObject go_PreviewPrefab;
}

public class CraftManual : MonoBehaviour
{
    //상태변수
    private bool isActivated = false;
    private bool isPreviewActivated = false;

    [SerializeField]
    private GameObject go_BaseUI;

    [SerializeField]
    private Craft[] craft_fire; // 모닥불 탭

    private GameObject go_Preview; // 미리보기 prefab 담을 변수
    private GameObject go_Prefab; // 실제 prefab

    [SerializeField]
    private Transform tf_Player;

    //Raycast
    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            Window();
        }

        if (isPreviewActivated)
            PreviewPositionUpdate();

        if(Input.GetButtonDown("Fire1"))
        {
            Build();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void Build()
    {
        if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable()) 
        {
            Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
            Destroy(go_Preview);
            BuildReset();
        }
    }


    private void PreviewPositionUpdate()
    {
        if (Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, range, layerMask))
        {
            if(hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                go_Preview.transform.position = _location;
            }
        }
    }

    //건축 취소
    private void Cancel()
    {
        if(isPreviewActivated)
            Destroy(go_Preview);

        BuildReset();
        go_BaseUI.SetActive(false);
    }


    public void SlotClick(int _slotNumber)
    {
        GameManager.isOpenCraftManual = false; 
        go_Preview = Instantiate(craft_fire[_slotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.forward, Quaternion.identity);
        go_Prefab = craft_fire[_slotNumber].go_Prefab;
        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }

    private void Window()
    {
        if (!isActivated)
            OpenWindow();
        else
            CloseWindow();
    }

    private void OpenWindow()
    {
        GameManager.isOpenCraftManual = true;
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        GameManager.isOpenCraftManual = false;
        isActivated = false;
        go_BaseUI.SetActive(false);
    }

    private void BuildReset()
    {
        isActivated = false;
        isPreviewActivated = false;
        go_Preview = null;
        go_Prefab = null;
    }
}
