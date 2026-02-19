using UnityEngine;

public class DamageDealerPlayer : MonoBehaviour
{
    [SerializeField] private int _damage;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            try
            {
                collision.gameObject.GetComponent<HealthController>().TakeDamage(_damage);
            }
            catch
            {
                Debug.LogWarning("Collision With Player with no player health controller");
            }
        }
    }
}

