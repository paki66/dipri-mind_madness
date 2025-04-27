using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    int damageAmount = 1;
    BoxCollider2D collider;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collider != null && other.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
            playerMovement.TakeDamage(damageAmount);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
