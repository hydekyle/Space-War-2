using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class ShipScript : HydeExtension
{
    Camera mainCamera;
    public float velocidadRotacion, disparoCD;
    public EZObjectPool bullets_pool;
    float disparo_lastTime = 0f;
    Bullet myBullet;
    Ship myShip;
    Stats myStats;
    public Transform cannonT;
    float _vida = 100f;
    float velocidadInicial;
    bool active = false;

    public float vida
    {
        get { return _vida; }
        set
        {
            _vida = value;
            if (_vida <= 0) Morir();
        }
    }

    private void Start()
    {
        vida = 100f;
        velocidadInicial = velocidadRotacion;
        mainCamera = Camera.main;
        bullets_pool = EZObjectPool.GetObjectPool("Bullets_1");
        myStats = new Stats()
        {
            velocity = 30 + myBullet.stats.velocity
        };
        active = true;
    }

    private void Update()
    {
        if (active)
        {
            InputListener();
            Control();
        }
    }

    float posX = 0f;
    float maxAngle = 0f;

    void Control()
    {
        velocidadRotacion = Input.GetKey(KeyCode.Mouse0) ?
            Mathf.Lerp(velocidadRotacion, velocidadInicial / 4, Time.deltaTime * velocidadInicial) :
            Mathf.Lerp(velocidadRotacion, velocidadInicial, Time.deltaTime * velocidadInicial);

        var inputX = Input.GetAxis("Horizontal");
        posX += inputX * Time.deltaTime * velocidadRotacion * 2;

        if (Mathf.Abs(posX) >= 8f && Math.Abs(inputX) > 0.9f) //Si llega al extremo y sigues moviendo, aumenta el ángulo máximo.
        {
            maxAngle -= inputX * Time.deltaTime * velocidadRotacion * 6;
            maxAngle = Mathf.Clamp(maxAngle, -120f, 120f);
        }
        else
        {
            maxAngle = Mathf.Lerp(maxAngle, 0f, Time.deltaTime * velocidadRotacion * 2);
        }

        posX = Mathf.Clamp(posX, -8f, 8f);
        float y = Mathf.Pow(posX, 2f) / 10;
        var dir = new Vector3((-posX + maxAngle) * 3.3f, 17, 0); //Girar según posX (y = "sensibilidad")
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, transform.forward), Time.deltaTime * velocidadInicial);
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, y, 0), Time.deltaTime * velocidadRotacion);
    }

    Vector3 dirInput;
    void InputListener()
    {
        //if (Input.GetKeyDown(KeyCode.F1)) GetDamage(15f);
        if (Input.GetKey(KeyCode.Mouse0)) Disparar();
    }

    public void Disparar()
    {
        if (Time.time > disparo_lastTime + disparoCD)
        {
            if (bullets_pool.TryGetNextObject(cannonT.position, transform.rotation, out GameObject go))
            {
                go.GetComponent<Rigidbody2D>().velocity = transform.right * myStats.velocity * 10; //No gustar
                go.GetComponent<TrailRenderer>().Clear();
                var myps = go.GetComponent<ParticleSystem>();
                myps.Play();
                foreach (Transform t in go.transform)
                {
                    var ps = t.GetComponent<ParticleSystem>();
                    ps.time = 0f;
                    ps.Play();
                }
                //StartCoroutine(DisableOnTime(go, 1f));
            }
            disparo_lastTime = Time.time;
        }
    }

    private void LookToMouse()
    {
        var dir = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, transform.forward), Time.deltaTime * velocidadRotacion);
    }

    void GetDamage(float dmg)
    {
        vida -= dmg;
        CanvasMenu.Instance.SetVidaUI(vida);
    }

    void Morir()
    {
        print("Me muero");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            GetDamage(10);
        }
    }

}
