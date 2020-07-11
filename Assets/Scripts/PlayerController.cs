using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject gameOver;
    public float speed = 7f;
    private Rigidbody2D rb;
    private float health = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        gameOver.SetActive(false);
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput, 0) * speed;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (health < 0.0f)
        {
            Debug.Log("Game Over");
            health = 0.0f;

            gameOver.SetActive(true);
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //gameObject.GetComponent<SliderJoint2D>().enabled = false;
            //rb.AddForce(new Vector2(17, 0), ForceMode2D.Impulse);
            gameObject.SetActive(false);


        }
        else
        {
            if (health > 1.0f)
                health = 1.0f;

            if (other.collider.name.StartsWith("Virus"))
            {
                health -= 0.25f;
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.white, health);
            }
            else if (other.collider.name.StartsWith("Desinfektionsmittel"))
            {
                health += 0.2f;
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.white, health);
            }
            else if (other.collider.name.StartsWith("Schutzmaske"))
            {
                health += 0.1f;
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.white, health);
            }
        }
    }
}
