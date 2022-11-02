using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed;

    public float acceleration;

    public float drag;

    public float angularSpeed;

    public float offsetBullet;

    public GameObject bulletPrefab;

    public float shootRate = 0.5f;

    private Rigidbody2D rb;

    private float vertical;

    private float horizontal;

    private bool shooting;

    private bool canShoot = true;

    public float vida;

    public Slider barraVida;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = drag;
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.value = vida;

        vertical = InputManager.Vertical;
        horizontal = InputManager.Horizontal;
        shooting = InputManager.Fire;

        Rotate();
        Shoot();

        if (vida <= 0)
        {
            Lose();
        }
    }

    void FixedUpdate()
    {
        var forwardMotor = Mathf.Clamp(vertical, 0f, 1f);
        rb.AddForce(transform.up * acceleration * forwardMotor);
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void Rotate () {
        if (horizontal == 0) {
            return;
        }
        transform.Rotate (0, 0, -angularSpeed * horizontal * Time.deltaTime);
    }

    private void Shoot()
    {
        if (shooting && canShoot)
        {
            StartCoroutine(FireRate());
            
        }
    }

    public void Lose()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }


    private IEnumerator FireRate()
    {
        canShoot = false;
        var pos = transform.up * offsetBullet + transform.position;

        var bullet = Instantiate(
            bulletPrefab, pos, transform.rotation
            );
        Destroy(bullet, 5);
        yield return new WaitForSeconds(shootRate);
        canShoot = true;
    }
    // public void PerderVida()
    //{

    //  if (CompareTag("Asteroid"))
    //   {
    //     vida = vida - 1;
    //   }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {
            vida = vida -= 1;
        }
    }
}
