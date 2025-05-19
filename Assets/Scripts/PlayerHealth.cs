using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1;
    private CheckPoint checkpointSystem;

    void Start()
    {
        // Busca el objeto que tiene el script CheckPoint
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
        if (checkpointSystem != null)
        {
            checkpointSystem.RespawnPlayer();
            health = 1; // Reinicia la vida
        }
    }
}
