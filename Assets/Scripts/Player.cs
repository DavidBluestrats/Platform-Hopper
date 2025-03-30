using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float halfWidth;
    public float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
         halfWidth = Camera.main.aspect * Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        transform.position = transform.position + mov;
        if (transform.position.x < -halfWidth)
        {
            transform.position = new Vector3(halfWidth, transform.position.y);
        }
        if (transform.position.x > halfWidth)
        {
            transform.position = new Vector3(-halfWidth, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "GameOver" || col.gameObject.tag == "Enemy")
        {
            gameObject.name = "GameOver";
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //dead = true;
        }
    }
}
