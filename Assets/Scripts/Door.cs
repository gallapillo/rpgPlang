using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : MonoBehaviour
{
    [Header("Door variables")]
    public bool PlayerInRange;
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;


    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(PlayerInRange && thisDoorType == DoorType.key)
            {
                // if have key
                if(playerInventory.numberOfkeys > 0)
                {
                    playerInventory.numberOfkeys--;
                    // we call open method
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
    }
    public void Close()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
            
        }
    }
}
