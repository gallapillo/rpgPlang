using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody2D, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody2D, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody2d, float knocktime)
    {
        if (myRigidbody2d != null)
        {
            yield return new WaitForSeconds(knocktime);
            myRigidbody2d.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody2d.velocity = Vector2.zero;
        }
    }
}
