using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum UpgradeType
{
    None,
    FiringSpeed,
    MagazineSize,
    Damage
}

public class XPManager : MonoBehaviour
{
    public Image xpBarImage; 

    public TextMeshProUGUI xpText;

    [SerializeField] private int xpPerKill = 10;
    [SerializeField] private int xpToNextLevel = 100;

    private int currentXP = 0;
    private int currentLevel = 1;
    private UpgradeType selectedUpgrade = UpgradeType.None;

    public GameObject upgradePanel;
    public Button firingSpeedButton;
    public Button magazineSizeButton;
    public Button damageButton;

    private void Start()
    {
        if (xpBarImage == null || xpText == null || upgradePanel == null)
        {
            Debug.LogError("XP bar, text, or upgrade panel not assigned in the inspector!");
        }

        UpdateXPUI();
    }

    private void UpdateXPUI()
    {
        float progress = (float)currentXP / xpToNextLevel;

        if (xpBarImage != null)
        {
            xpBarImage.transform.localScale = new Vector3(1f, progress, 1f);
        }

        if (xpText != null)
        {
            xpText.text = "XP: " + currentXP + " / " + xpToNextLevel;
        }
    }

    private void LevelUp()
    {
        currentXP = 0;
        currentLevel++;

        
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);

        
        ShowUpgradePanel();

        UpdateXPUI();
    }

    private void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }

    
    public void UpgradeFiringSpeed()
    {
        selectedUpgrade = UpgradeType.FiringSpeed;
        CloseUpgradePanel();
    }

    
    public void UpgradeMagazineSize()
    {
        selectedUpgrade = UpgradeType.MagazineSize;
        CloseUpgradePanel();
    }

    
    public void UpgradeDamage()
    {
        selectedUpgrade = UpgradeType.Damage;
        CloseUpgradePanel();
    }

    public void ApplyUpgrade()
    {
        
        switch (selectedUpgrade)
        {
            case UpgradeType.FiringSpeed:
                
                Debug.Log("Firing speed upgraded!");
                break;

            case UpgradeType.MagazineSize:
                
                Debug.Log("Magazine size upgraded!");
                break;

            case UpgradeType.Damage:
                
                Debug.Log("Damage upgraded!");
                break;

            

            default:
                break;
        }

        
        selectedUpgrade = UpgradeType.None;
    }

    private void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public void EarnXP()
    {
        currentXP += xpPerKill;
        UpdateXPUI();

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }
}
