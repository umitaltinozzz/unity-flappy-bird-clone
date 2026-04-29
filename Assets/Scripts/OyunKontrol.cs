using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour
{
    public GameObject gokyuzu1;
    public GameObject gokyuzu2;

    Rigidbody2D rigidbody1_;
    Rigidbody2D rigidbody2_;

    public float arkaPlanHiz = -1.5f;
    float uzunluk;

    public GameObject engel;
    public int kacAdetEngel = 5;
    GameObject[] engeller;

    public float zamanSayac;
    int sayac = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody1_ = gokyuzu1.GetComponent<Rigidbody2D>();
        rigidbody2_ = gokyuzu2.GetComponent<Rigidbody2D>();

        Vector2 vector2 = new Vector2(arkaPlanHiz, 0);
        rigidbody1_.velocity = vector2;
        rigidbody2_.velocity = vector2;

        uzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x;
        engeller = new GameObject[kacAdetEngel];

        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector3(-20,-20), Quaternion.identity);

            Rigidbody2D rigidbodyEngel = engeller[i].AddComponent<Rigidbody2D>();
            rigidbodyEngel.velocity = vector2;
            rigidbodyEngel.gravityScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ArkaPlanHareket();
        EngelHareket();
    }

    void ArkaPlanHareket()
    {
        if (gokyuzu1.transform.position.x <= -uzunluk * 2.5)
        {
            gokyuzu1.transform.position += new Vector3(uzunluk * 2, 0);
        }

        if (gokyuzu2.transform.position.x <= -uzunluk)
        {
            gokyuzu2.transform.position += new Vector3(uzunluk * 2, 0);
        }
    }

    void EngelHareket()
    {
        zamanSayac += Time.deltaTime;
        if (zamanSayac >= 2)
        {
            zamanSayac = 0;
            float Yekseni = Random.Range(1f, 3.5f);
            engeller[sayac].transform.position = new Vector3(3f, Yekseni);

            sayac++;
            if (sayac == 5)
            {
                sayac = 0;
            }
        }
    }

    public void OyunBitti()
    {
        Vector2 vec = new Vector2(0,0);

        rigidbody1_.velocity = vec;
        rigidbody2_.velocity = vec;

        for (int i = 0; i < engeller.Length; i++)
        {
            Rigidbody2D rigidbodyEngel = engeller[i].GetComponent<Rigidbody2D>();
            rigidbodyEngel.velocity = vec;
            rigidbodyEngel.gravityScale = 0;
        }
    }
}