using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    private Rigidbody2D rb;
    private float health;
    private long score;

    public Text healthLabel, scoreLabel;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;


        health = 1.0f;
        score = 0;
        if (healthLabel != null && scoreLabel != null)
        {
            healthLabel.text = "Health : 100%";
            scoreLabel.text = "Score : 0";
        }
        //transform.position = new Vector3(0,0,0);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (gameObject.name.StartsWith("PlayersAnchor"))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput, 0) * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // called whenever collision occurs on this gameObject
    public void OnCollisionEnter2D(Collision2D other)
    {

        // 1. Schritt : Gesundheitswert aktualisieren:
        if (other.collider.name.StartsWith("Virus"))
        {
            AudioManager.instance.PlaySound("PlayerDamage");
            health -= 0.25f;
            //StartCoroutine(FlashBackground());
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
            StartCoroutine(GameManager.instance.SetState(GameManager.State.GAMEOVER));
        }
        else if (health > 1.0f)
        {
            health = 1.0f;
        }

        // 3. Schritt : Gesundheit und Score anzeigen:
        healthLabel.text = "Health : " + Mathf.RoundToInt(health * 100) + "%";
        scoreLabel.text = "Score : " + score;
    }

    IEnumerator FlashBackground()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color c = renderer.color;
        for (float ft = 1f; ft > 0; ft -= 0.02f)
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, 1);
            c.a = ft;
            renderer.color = c;
            yield return null;
        }
    }

    public IEnumerator RemovePlayer()
    {
        Debug.Log(Time.time + " : " + "enter RemovePlayer()");
        // Collider und Constraints deaktivieren:
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpringJoint2D>().enabled = false;
        //gameObject.GetComponent<SliderJoint2D>().enabled = false;

        // Objekt einmaligen Kraft- und Drehmomentimpuls mitgeben (Purzeleffekt):
        rb.AddForce(Vector2.up * 4.6f, ForceMode2D.Impulse);
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
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
        transform.position = new Vector3(0f, -4.87f, 0f);

        Debug.Log(Time.time + " : " + "exit RemovePlayer()");
    }

    public IEnumerator ShowPlayer()
    {

        Debug.Log(Time.time + " : " + "enter ShowPlayer()");

        health = 1.0f;
        score = 0;
        healthLabel.text = "Health : 100%";
        scoreLabel.text = "Score : 0";


        gameObject.GetComponent<SpringJoint2D>().enabled = true;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        Debug.Log(Time.time + " : " + "exit ShowPlayer()");
    }


}
