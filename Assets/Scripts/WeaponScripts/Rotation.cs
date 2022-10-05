using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    private float SpriteOffset = 0.0f;

    private void Awake()
    {
        SpriteOffset = transform.GetComponentInChildren<WeaponController>().SpriteOffset;
    }

    void FixedUpdate()
    {
        //ROTATION
        if (transform.GetComponentInChildren<WeaponController>().isAtached == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 13;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            mousePosition.z = 0;

            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            //transform.eulerAngles = new Vector3(0, 0, angle + SpriteOffset);
            if (angle > 90 || angle < -90)
            {
                transform.eulerAngles = new Vector3(0, 180, 180 - angle + SpriteOffset);
            }
            else transform.eulerAngles = new Vector3(0, 0, angle + SpriteOffset);
        }
    }
}