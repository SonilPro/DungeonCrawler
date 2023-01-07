using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{

    [SerializeField] private GameObject heartContainer;
    private float fillValue;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameController.Instance.player.GetComponent<PlayerController>();
    }

    void Update()
    {
        float fillValue = (float)playerController.health / playerController.maxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}