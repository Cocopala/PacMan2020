using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    public AudioSource killSfx;
    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            killSfx = GetComponent<AudioSource>();
            audio.Play(KillSfx);
        }
    }
    // Start is called before the first frame update

}

