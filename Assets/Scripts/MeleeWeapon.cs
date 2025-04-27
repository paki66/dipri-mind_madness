using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] PlayerMovement player;


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(player.attack);
        }
    }
}
