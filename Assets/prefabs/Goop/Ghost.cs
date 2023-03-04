using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D( Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Fire"))
        { 
            Destroy(other.gameObject);

            health = health -1;
            GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            
            if(health <= 0)
                {  Destroy(this.gameObject);
                    CurrentRoom.gameObject.SendMessage("RoomClear");
                }
        }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
            

            health = health -10;
            
            if(health <= 0)
                {  Destroy(this.gameObject);
                    
                }
        }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
            Destroy(this.gameObject);     
        }

        if(other.gameObject.CompareTag("Player"))
        {
           
            
            animator.Play("GoopAttack");
            
            //other.gameObject.SendMessage("EnemyCollide");
            

        }
    }
}
