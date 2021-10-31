using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //공유자원, 클래스 변수 = 정적변수 / 무기 중복 교체 실행 방지
    public static bool isChangeWeapon = false;
    //현재 무기와 애니메이션
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;

    //현재 무기 타입
    [SerializeField]
    private string currentWeaponType;

    //무기 교체 딜레이
    [SerializeField]
    private float changeWeaponDelayTime;
    //무기 교체 끝나는 시점
    [SerializeField]
    private float changeWeaponEndDelayTime;

    // 무기류
    [SerializeField]
    private Gun[] guns;
    [SerializeField]
    private Hand[] hands;

    //무기 접근 관리
    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, Hand> handDictionary = new Dictionary<string, Hand>();

    [SerializeField]
    private GunController theGunController;
    [SerializeField]
    private HandController theHandController;

    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].handName, hands[i]);
        }
    }

    
    void Update()
    {
        if(!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(ChangeWeaponCorutine("HAND", "맨손"));

            else if (Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(ChangeWeaponCorutine("GUN", "SubMachineGun1"));
        }
    }

    public IEnumerator ChangeWeaponCorutine(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    private void CancelPreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "GUN":
                theGunController.CancelFineSight();
                theGunController.CancelReload();
                GunController.isActivate = false;
                break;
            case "HAND":
                HandController.isActivate = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if(_type == "GUN")
            theGunController.GunChange(gunDictionary[_name]);

        else if(_type == "HAND")
            theHandController.HandChange(handDictionary[_name]);
    }
}
