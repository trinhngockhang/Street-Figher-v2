using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour {
    private bool first = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "redBrick")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "grayBrick")
        {
            Destroy(gameObject);
        }
        if ( collision.gameObject.tag != "normalBullet")
        {
            if(!first)
            {
                gameObject.tag = "normalBulletReady";
                Destroy(gameObject);
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            first = false;
        }
    }
}
