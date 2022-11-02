using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour

{
    private Rigidbody2D rb;

    public float speed;

    public GameObject[] subAsteroids;

    public int numberOfAsteroids;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 0;
        rb.angularDrag = 0;

        rb.velocity = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            0
            ).normalized * speed;

        rb.angularVelocity = Random.Range(-50f, 50f);
    }

    private bool isDestroying = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(isDestroying)
            return;
       if (col.CompareTag ("Bullet"))
        {
            PuntajeManager.puntajes += 100;
            isDestroying = true;
            Destroy(gameObject);
            Destroy(col.gameObject);
            for (var i = 0; i < numberOfAsteroids; i++)
            {
                Instantiate(
                    subAsteroids[Random.Range(0, subAsteroids.Length)],
                    transform.position,
                    Quaternion.identity
                );
            }
        } 
       if (col.CompareTag("Player"))
        {
            var asteroids = FindObjectsOfType<Asteroids>();

            for (var i = 0; i < asteroids.Length; i++)
            {
                Destroy(asteroids[i].gameObject);
            }
          //  col.GetComponent<PlayerControl>().PerderVida();
        }
    }
   

}
