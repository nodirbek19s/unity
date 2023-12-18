using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float health = 3f; // Health of the enemy
    public TextMeshProUGUI healthText; 
    public float moveSpeed = 5f; // Movement speed towards the player
    public float detectionRange = 10f; // Range at which the enemy starts moving towards the player
    private Transform playerTransform; // Player's transform

    private void Start()
    {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            GameObject healthTextObj = GameObject.FindGameObjectWithTag("healthbar");
            if (healthTextObj != null)
            {
                healthText = healthTextObj.GetComponent<TextMeshProUGUI>();
            }
            UpdateHealthDisplay();
    }


    private void Update()
    {
        UpdateHealthDisplay();
        // Move towards the player if within range
        if (Vector3.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Make the enemy face the player
            transform.LookAt(playerTransform.position);

            // Move the enemy towards the player
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        }
    }


    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString();
            Canvas.ForceUpdateCanvases(); // Force the Canvas to update
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Add this line

        // Check for collision with a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Assuming bullets do 1 damage
            TakeDamage(1f);
            Destroy(collision.gameObject); // Destroy the bullet
        }
        // Check for collision with the player
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Enemy wins, trigger win logic here
            Debug.Log("Enemy wins!");
            // Destroy the enemy for simplicity
            Destroy(gameObject);
            SceneManager.LoadScene(1);

        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthDisplay();

        if (health <= 0)
        {
            Destroy(gameObject); // Enemy is destroyed
        }
       
    }
}
