using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool PlayerInRange;
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerInRange && !isOpen)
        {
            if (dialogBox.activeInHierarchy)
            {
                //open
                dialogBox.SetActive(false);
                OpenChest();
            }
            else
            {
                // is already open
                ChestAlreadyOpen();
            }

        }
    }

    public void OpenChest()
    {
        //Open
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        anim.SetBool("Opened", true);
        isOpen = true;
        //raise
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(true);
        dialogText.text = dialog;
        playerInventory.currentItem = null;
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
            dialogBox.SetActive(false);
        }
    }
}
