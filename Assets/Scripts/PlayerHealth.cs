// PlayerHealth.cs
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("El jugador recibio dano. Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡Jugador muerto!");
        // Puedes anadir animacion o recargar escena
        Destroy(gameObject);
    }
}
