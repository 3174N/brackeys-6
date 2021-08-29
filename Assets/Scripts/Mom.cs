using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : MonoBehaviour
{
    private bool _isInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            _isInRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            _isInRange = false;
        }
    }

    private void Update()
    {
        if (_isInRange && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
