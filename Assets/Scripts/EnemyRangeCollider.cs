using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangeCollider : MonoBehaviour
{
    public bool follow = false;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("PlayerWrapper").transform;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerWrapper")
        {
            follow = true;
        }
    }

}
