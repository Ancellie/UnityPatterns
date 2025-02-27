using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Event dropped;
    public Event pickedUp;
    public Image icon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropped.Occurred(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickedUp.Occurred(this.gameObject);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = true;
            Destroy(this.gameObject, 5);

        }
    }
    
   
}
