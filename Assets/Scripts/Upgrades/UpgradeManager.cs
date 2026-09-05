using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private XPSystem xpSystem;
    [SerializeField] private WeaponController weaponController;

    [SerializeField] private UpgradeData[] availableUpgrades;

    [Header("UI")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button[] upgradeButtons;
    [SerializeField] private TMP_Text[] upgradeTexts;

    private UpgradeData[] currentChoices = new UpgradeData[3];

    private void OnEnable()
    {
        xpSystem.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        xpSystem.OnLevelUp -= HandleLevelUp;
    }

    private void Start()
    {
        upgradePanel.SetActive(false);
    }


    private void HandleLevelUp(int newLevel)
    {
        Time.timeScale = 0f;
        ShowUpgradeChoices();
    }

    private void ShowUpgradeChoices()
    {
        upgradePanel.SetActive(true);

        // Create a temporary list of all available upgrades
        UpgradeData[] shuffledUpgrades =
            new UpgradeData[availableUpgrades.Length];

        for (int i = 0; i < availableUpgrades.Length; i++)
        {
            shuffledUpgrades[i] = availableUpgrades[i];
        }

        // Shuffle the list
        for (int i = 0; i < shuffledUpgrades.Length; i++)
        {
            int randomIndex =
                Random.Range(i, shuffledUpgrades.Length);

            UpgradeData temp = shuffledUpgrades[i];

            shuffledUpgrades[i] =
                shuffledUpgrades[randomIndex];

            shuffledUpgrades[randomIndex] = temp;
        }

        // Take the first 3 unique upgrades
        for (int i = 0; i < currentChoices.Length; i++)
        {
            currentChoices[i] = shuffledUpgrades[i];

            upgradeTexts[i].text =
                currentChoices[i].upgradeName +
                "\n" +
                currentChoices[i].description;
        }
    }

    public void SelectUpgrade(int index)
    {
        if (index < 0 || index >= currentChoices.Length)
            return;

        UpgradeData selectedUpgrade = currentChoices[index];

        weaponController.ApplyUpgrade(selectedUpgrade);

        Debug.Log($"Selected Upgrade: {selectedUpgrade.upgradeName}");

        upgradePanel.SetActive(false);

        Time.timeScale = 1f;
    }
}