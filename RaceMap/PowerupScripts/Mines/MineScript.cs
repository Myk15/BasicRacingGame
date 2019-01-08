using UnityEngine;

public class MineScript : MonoBehaviour
{
    public int Damage;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Collider>().tag == "Player")
        {
            other.collider.GetComponent<PlayerInformationScript>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
