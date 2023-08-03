using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
     public float activeDuration = 1f; 
    public float inactiveDuration = 2f; 
    public int damageAmount = 10; 

    private Animator animator;
    private BoxCollider boxCollider;

    private bool isActive = false;
    private float timer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isActive)
        {
            if (timer >= activeDuration)
            { 
                DeactivateTrap();
            }
        }
        else
        { 
            if (timer >= inactiveDuration)
            {
                ActivateTrap();
            }
        }
    }

    void ActivateTrap()
    {
        isActive = true;
        timer = 0f;
        animator.SetTrigger("Activate");
        boxCollider.enabled = true;
    }

    void DeactivateTrap()
    {
        isActive = false;
        timer = 0f;
        animator.SetTrigger("Deactivate");
        boxCollider.enabled = false;
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            PlayerStatusManager playerStatus = collision.gameObject.GetComponent<PlayerStatusManager>();

             Debug.Log("Player hit by fire.");
            if (playerStatus != null)
            {
                playerStatus.health -= damageAmount;
            }
        }
    }
}
