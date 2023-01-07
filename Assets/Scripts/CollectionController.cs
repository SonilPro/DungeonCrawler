using UnityEngine;

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

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameController.Instance.player.GetComponent<PlayerController>();
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<BoxCollider2D>());
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerController.HealPlayer(healthChange);
            Destroy(gameObject);
        }
    }
}