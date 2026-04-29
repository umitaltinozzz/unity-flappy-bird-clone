using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kusKontrol : MonoBehaviour
{
    public Sprite[] kusSprites;
    SpriteRenderer spriteRenderer;
    bool ileriHareket = true;
    int kusSayacHareket = 0;
    float kusAnimasyonZaman = 0;
    Rigidbody2D rigidbody_;
    public Text sayacText;
    int sayacPuan = 0;
    bool oyunDevamEdiyor = true;
    public GameObject oyunKontrol;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody_ = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunKontrol");
    }

    // Update is called once per frame
    void Update()
    {
        KusAnimasyon();
        KusHareket();
    }

    void KusAnimasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;

        if (kusAnimasyonZaman > 0.1f)
        {
            kusAnimasyonZaman = 0;
            if (ileriHareket)
            {
                spriteRenderer.sprite = kusSprites[kusSayacHareket];

                kusSayacHareket++;
                if (kusSayacHareket == 3)
                {
                    kusSayacHareket--;
                    ileriHareket = false;
                }
            }
            else
            {
                spriteRenderer.sprite = kusSprites[kusSayacHareket];
                if (kusSayacHareket == 0)
                {
                    kusSayacHareket++;
                    ileriHareket = true;
                }
            }
        }
    }

    void KusHareket()
    {
        if (Input.GetMouseButtonDown(0) && oyunDevamEdiyor)
        {
            rigidbody_.velocity = new Vector2(0, 0);
            rigidbody_.AddForce(new Vector2(0, 200));
        }

        if (rigidbody_.velocity.y > 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -30);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puan"))
        {
            sayacPuan++;
            sayacText.text = "SKOR: " + sayacPuan;
        }

        if (collision.gameObject.CompareTag("engel"))
        {
            oyunDevamEdiyor = false;

            oyunKontrol.GetComponent<OyunKontrol>().OyunBitti();
            transform.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}