using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{

    [SerializeField] private GameObject heartContainer;
    private float fillValue;
    [SerializeField] private PlayerController playerController = default;

    void Update()
    {
        float fillValue = (float)playerController.health / playerController.maxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}