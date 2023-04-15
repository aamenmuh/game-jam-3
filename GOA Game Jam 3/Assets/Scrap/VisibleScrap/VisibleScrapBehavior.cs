using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleScrapBehavior : MonoBehaviour
{
    private ScrapData data;
    private SpriteRenderer render;
    public float timer = 1f;
    private InventoryManager manager;
    [SerializeField] private float dropForce;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        data = GetComponent<ScrapData>();
        render.sprite = data.sprite;
        manager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
        rb = GetComponent<Rigidbody2D>();
        float angle = Random.Range(0, 360);
        Vector2 force = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dropForce;
        rb.AddForce(force);
    }
    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer < 0f)
        {
            if(manager.Add(this.gameObject)) Destroy(this.gameObject);
        }
    }
}
