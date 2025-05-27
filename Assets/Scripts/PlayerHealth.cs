using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1;
    private CheckPoint checkpointSystem;

    void Start()
    {
        checkpointSystem = FindObjectOfType<CheckPoint>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("El jugador recibió daño. Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡Jugador muerto!");

        if (DeathManager.Instance != null)
        {
            DeathManager.Instance.AddDeath();

            if (DeathManager.Instance.deathCount >= 10)
            {
                // Llegó a 10 muertes, se lanza el Game Over
                // No hacemos respawn aquí para evitar conflicto
                return;
            }
        }

        // Si no se llegó a 10 muertes, respawneamos normalmente
        if (checkpointSystem != null)
        {
            checkpointSystem.RespawnPlayer();
            health = 1; // Reinicia la vida
        }
    }
}
