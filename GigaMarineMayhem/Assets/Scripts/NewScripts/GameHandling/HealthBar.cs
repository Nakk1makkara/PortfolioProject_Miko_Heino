using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage; 
    public TextMeshProUGUI healthText; 

    private void Start()
    {
        if (healthBarImage == null || healthText == null)
        {
            Debug.LogError("Health bar image or TMP text not assigned in the inspector");
        }
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (healthBarImage != null)
        {
            
            float healthPercentage = (float)currentHealth / maxHealth;

           
            healthBarImage.transform.localScale = new Vector3(healthPercentage, 1f, 1f);
        }

        if (healthText != null)
        {
            
            healthText.text = currentHealth.ToString();
        }
    }
}
