using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{


    public Vector3 direc;

    public System.Action destroyed;// varaible for store function to invoke later bu using the + to  add the list of the functions

    public float speed;


    // Update is called once per frame
    void Update()
    {

transform.position += direc * speed * Time.deltaTime;

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (destroyed != null)
        {

            this.destroyed.Invoke();//invoke this destroy when the shoot 
        }
       

        Destroy(gameObject);

    }
}
 