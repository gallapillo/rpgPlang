using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knocktime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable"))
        {
            other.GetComponent<pot>().Smash();
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 diffence = hit.transform.position - transform.position;
                diffence = diffence.normalized * thrust;
                hit.AddForce(diffence, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("enemy"))// stoe && other.isTrigger
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knocktime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knocktime, damage);
                    }
                }
                
               
            }
        }
    }

 
}
