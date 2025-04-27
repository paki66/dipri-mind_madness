using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weapon;
   
    public void EnableWeaponCollider(int isEnable)
    {
        if (weapon != null)
        {
            var col = weapon.GetComponent<Collider2D>();
            if (col != null)
            {
                if (isEnable == 1)
                {
                    col.enabled = true;
                }
                else
                {
                    col.enabled = false;
                }
            }
        }
    }

}
