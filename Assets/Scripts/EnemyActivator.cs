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

        if (other.tag == "Car")
        {
            other.gameObject.GetComponent<Car>().enabled = true;
            other.gameObject.transform.Find("pCube1").gameObject.SetActive(true);
            other.gameObject.GetComponent<Car>().Activate(transform.position.z);
        }
    }
}
