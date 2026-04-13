using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    public float damage = 20f; 
    void Start()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            return;

        ENEMY enemy = collision.gameObject.GetComponentInParent<ENEMY>();
        if (enemy != null)
            enemy.TakeDamage(damage);

        if (hitEffectPrefab != null)
        {
            ContactPoint contact = collision.contacts[0];
            GameObject effect = Instantiate(hitEffectPrefab, contact.point,
                Quaternion.LookRotation(contact.normal));
            Destroy(effect, 2f);
        }

        Destroy(gameObject);
    }
}