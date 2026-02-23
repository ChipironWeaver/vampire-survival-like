using UnityEngine;

public class DamageDealerPlayer : MonoBehaviour
{
    [SerializeField] private int _damage;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthController>().TakeDamage(_damage);
        }
    }
}

