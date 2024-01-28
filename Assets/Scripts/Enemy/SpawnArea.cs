using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnManager.isSpawned = true;
            SpawnManager.colliderObj = gameObject;

            if(dialogueTrigger!= null)
            {
                dialogueTrigger.StartDialogue();
            }
        }
    }
}
