using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    private bool isSuper = false;
    private GameObject gameManagement;
    
    void Start()
    {
        gameManagement = GameObject.FindGameObjectWithTag("Management");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManagement.Instance.EatenDot(isSuper);
            gameManagement.GetComponent<GameManagement>().Score++;
            Destroy(gameObject);
           
        }
    }

    public void MakeDotSuper()
    {
        isSuper = true;
        transform.localScale = new Vector3(3f, 3f, 3f);
    }




}

