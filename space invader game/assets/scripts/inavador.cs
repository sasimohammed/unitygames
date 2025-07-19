using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inavador : MonoBehaviour
{
    public Sprite[] sprites;//array for object images 





    public System.Action killed ;
  

    public float animationtime=1.0f;

    private SpriteRenderer _SpriteRenderer;// the thing that will render the image of the object

    private int animationframe;//index for the images 

    private void Awake()//run before the start
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();//get the image in my object
    }

    private void Start()
    {
        InvokeRepeating(nameof(animatesprite),this.animationtime,this.animationtime);//for repeating 

    }
    private void animatesprite()
    {
        animationframe++;// like index for the sprite choosen 
        if (animationframe >= sprites.Length)//for none boundry errors
        {
            animationframe = 0;


        }

        _SpriteRenderer.sprite = this.sprites[animationframe];//access teh index for each animantion

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.layer == LayerMask.NameToLayer("laser"))

        {
            killed.Invoke();

            gameObject.SetActive(false);


        }

    }
    
        
    






}
