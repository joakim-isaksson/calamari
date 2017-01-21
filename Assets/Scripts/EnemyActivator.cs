using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().enabled = true;
            other.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
