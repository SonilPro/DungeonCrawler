using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    public static bool isDead = false;

    private void Update()
    {
        Debug.Log("UPDATED");
        if (isDead)
        {
            deathScreen.SetActive(true);
        }
    }


}
