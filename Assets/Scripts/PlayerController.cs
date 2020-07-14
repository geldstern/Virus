using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public GameObject gameOver;
    public float speed = 7f;
    private Rigidbody2D rb;
    private float health;
    private long score;

    public Text healthLabel, scoreLabel;


    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        health = 1.0f;
        score = 0;
        healthLabel.text = "Health : 100%";
        scoreLabel.text = "Score : 0";
        StartCoroutine(ShowPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput, 0) * speed;
        }
    }


    // called whenever collision occurs on this gameObject
    public void OnCollisionEnter2D(Collision2D other)
    {

        // 1. Schritt : Gesundheitswert aktualisieren:
        if (other.collider.name.StartsWith("Virus"))
        {
            AudioManager.instance.PlaySound("PlayerDamage");
            health -= 0.25f;
        }
        else if (other.collider.name.StartsWith("Desinfektionsmittel"))
        {
            AudioManager.instance.PlaySound("PlayerSuccess1");
            health += 0.2f;
            score += 1000;
        }
        else if (other.collider.name.StartsWith("Schutzmaske"))
        {
            AudioManager.instance.PlaySound("PlayerSuccess2");
            health += 0.1f;
            score += 200;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.white, health);

        // 2. Schritt : Limits überprüfen:
        if (health <= 0.0f)
        {
            AudioManager.instance.PlaySound("PlayerDeath");
            health = 0.0f;
            StartCoroutine(RemovePlayer());
        }
        else if (health > 1.0f)
        {
            health = 1.0f;
        }

        // 3. Schritt : Gesundheit und Score anzeigen:
        healthLabel.text = "Health : " + Mathf.RoundToInt(health * 100) + "%";
        scoreLabel.text = "Score : " + score;
    }
    IEnumerator RemovePlayer()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SliderJoint2D>().enabled = false;
        rb.AddForce(Vector2.up * 7.6f, ForceMode2D.Impulse);
        rb.AddTorque(7.6f, ForceMode2D.Impulse);

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color c = renderer.color;
        for (float ft = 1f; ft > 0; ft -= 0.02f)
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, 1);
            c.a = ft;
            renderer.color = c;
            yield return null;
        }

        StartCoroutine(FadeIn());
        Debug.Log("Game Over");
    }

    /*
        IEnumerator ShowPlayer()
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SliderJoint2D>().enabled = false;

            //rb.AddForce(Vector2.up * 7.6f, ForceMode2D.Impulse);
            //rb.AddTorque(7.6f, ForceMode2D.Impulse);
            while (transform.position.y < -3.7f)
            {
                transform.Translate(0, 0.05f, 0, Space.World);
                yield return null;
            }

            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SliderJoint2D>().enabled = true;
        }
    */

    IEnumerator ShowPlayer()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SliderJoint2D>().enabled = false;
        gameObject.GetComponent<TargetJoint2D>().enabled = true;

        //rb.AddForce(Vector2.up * 7.6f, ForceMode2D.Impulse);
        //rb.AddTorque(7.6f, ForceMode2D.Impulse);


        yield return new WaitForSeconds(2f);


        gameObject.GetComponent<TargetJoint2D>().enabled = false;
        gameObject.GetComponent<SliderJoint2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

    }




    IEnumerator FadeIn()
    {
        Text text = gameOver.GetComponent<Text>();
        Color c = text.color;
        c.a = 0f;
        text.color = c;
        gameOver.SetActive(true);
        for (float ft = 0f; ft < 1; ft += 0.1f)
        {
            c.a = ft;
            text.color = c;
            yield return new WaitForSeconds(.1f);
        }
        gameOver.SetActive(true);
        Debug.Log("Game Over");

    }
}
