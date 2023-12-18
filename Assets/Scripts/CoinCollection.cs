using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinCollection : MonoBehaviour
{
    private int Coin = 0;
    public TextMeshProUGUI coinText;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the collision has the "coin" tag
        if (other.CompareTag("coin"))
        {
            // Check if the object is not an enemy (assuming the enemy is tagged as "Enemy")
            if (!other.CompareTag("Enemy"))
            {
                Coin++;
                coinText.text = "Coin: " + Coin.ToString();
                Debug.Log("Checking whether coin is collected " + Coin);
                Destroy(other.gameObject);

                // Check if the coin count is more than 10
                if (Coin > 30)
                {
                    // Load the next scene
                    LoadNextScene();
                }
            }
        }
    }

    private void LoadNextScene()
    {
        // Load the next scene by name
        SceneManager.LoadScene(0);
    }
}
