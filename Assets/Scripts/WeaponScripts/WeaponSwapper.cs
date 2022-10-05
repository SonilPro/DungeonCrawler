using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapper : MonoBehaviour
{
    [SerializeField] private GameObject mainWeapon = default;
    [SerializeField] private GameObject offHandWeapon = default;
    private GameObject activeWeapon;

    float lastFire = 0f;

    void Start()
    {
        activeWeapon = mainWeapon;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SwapWeapon();
        }
    }

    private void OnTriggerStay(Collider target)
    {

        if (target.tag == "Weapon" && Input.GetKeyDown(KeyCode.Mouse0) && Time.time > (lastFire + 0.2f))
        {
            //Check if a weapon is already equipped
            if (activeWeapon.transform.childCount != 0)
            {

                if (activeWeapon == mainWeapon && offHandWeapon.transform.childCount == 0 || (activeWeapon == offHandWeapon && mainWeapon.transform.childCount == 0))
                {
                    SwapWeapon();
                }
                else
                {
                    DropWeapon(target);
                }
            }
            PickupWeapon(target);
        }
    }

    void SwapWeapon()
    {

        mainWeapon.SetActive(!mainWeapon.activeInHierarchy);
        offHandWeapon.SetActive(!offHandWeapon.activeInHierarchy);

        if (mainWeapon.activeInHierarchy == true) activeWeapon = mainWeapon;
        else activeWeapon = offHandWeapon;

        Debug.Log(activeWeapon.name);
    }

    void PickupWeapon(Collider target)
    {

        target.GetComponentInChildren<WeaponController>().isAtached = true;
        target.transform.position = new Vector3(activeWeapon.transform.position.x, activeWeapon.transform.position.y + (target.GetComponentInChildren<SpriteRenderer>().sprite.pivot.y / 32 * 3 / 8), activeWeapon.transform.position.z);
        target.transform.SetParent(activeWeapon.transform);
        lastFire = Time.time;
    }

    void DropWeapon(Collider target)
    {

        GameObject child = activeWeapon.transform.GetChild(0).gameObject;
        child.GetComponentInChildren<WeaponController>().isAtached = false;
        //Position
        child.transform.position = target.transform.position;
        //Rotation
        child.transform.rotation = Quaternion.Euler(Vector3.zero);
        child.transform.SetParent(null);
    }
}