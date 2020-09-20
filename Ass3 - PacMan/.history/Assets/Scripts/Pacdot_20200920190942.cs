using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            audio.Play(KillSfx);
        }
    }
    // Start is called before the first frame update

}


public class KillSfx : MonoBehaviour
{
    public AudioSource killSfx;
    void Start()
    {
        killSfx = GetComponent<AudioSource>();
    }
}
