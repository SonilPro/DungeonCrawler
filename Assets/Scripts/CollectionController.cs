﻿using UnityEngine;

[System.Serializable]

public class Item
{
    public string name;
    public string description;
    public Sprite itemImage;
}

public class CollectionController : MonoBehaviour
{

    [SerializeField] private Item item;
    [SerializeField] private float healthChange;

    [SerializeField] private PlayerController playerController;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<BoxCollider>());
        gameObject.AddComponent<BoxCollider>();
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            playerController.HealPlayer(healthChange);
            Destroy(gameObject);
        }
    }
}