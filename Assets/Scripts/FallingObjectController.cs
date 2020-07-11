using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectController : MonoBehaviour
{
    public bool isRotating = true;
    public bool isScaling = true;
    // Winkelgeschwindigkeit
    public bool isShifting = true;

    private float timeShift;
    private float angularVelocity;
    // Start is called before the first frame update
    private Vector3 initialScale;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        angularVelocity = Random.Range(20, 45) * Mathf.Sign(Random.Range(-1, 1));
        initialScale = transform.localScale;
        timeShift = Random.Range(0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation:
        if (isRotating)
        {
            rb.angularVelocity = angularVelocity;
        }
        // Wabereffekt:
        if (isScaling)
        {
            transform.localScale = new Vector3(initialScale.x + 0.1f * Mathf.Sin(timeShift + Time.time * 10),
                    initialScale.y + 0.1f * Mathf.Cos(timeShift + Time.time * 10), 1);
        }
        // Schwebeeffekt:
        if (isShifting)
        {
            rb.velocity = new Vector2(2f * Mathf.Sin(timeShift + Time.time * 10), rb.velocity.y);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision : " + gameObject.name + " / " + other.collider.name);
        if (gameObject.name.StartsWith("Virus"))
        {
            if (other.collider.name.StartsWith("Floor"))
            {
                Destroy(gameObject);
            }
            else if (other.collider.name.StartsWith("Player"))
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.name.StartsWith("Desinfektion") || gameObject.name.StartsWith("Schutzmaske")) 
        {
            if (other.collider.name.StartsWith("Floor"))
            {
                Destroy(gameObject);
            }
            else if (other.collider.name.StartsWith("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
