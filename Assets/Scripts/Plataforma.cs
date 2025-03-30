using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float force = 0f;
    
    private void Update()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        if (transform.position.y < stageDimensions.y)
        {
            Destroy(gameObject);
        } 
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Rigidbody2D Rig = col.gameObject.GetComponent<Rigidbody2D>();
        if (col.relativeVelocity.y <= 0.001f)
        {
            if (col.gameObject.tag == "Player")
            {
                Rig.velocity = new Vector2(0, force);
            }
        }
    }
    
}
