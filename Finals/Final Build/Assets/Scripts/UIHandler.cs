using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Resources")]
    public TextMeshProUGUI goldAmountText;

    [Header("Life")]
    public TextMeshProUGUI lifeText;
    public Image lifeBarFill;

    [Header("Tower Info")]
    public GameObject upgradePanel;
    public TextMeshProUGUI towerNameText;

    void Update()
    {
        goldAmountText.text = GameManager.instance.gold.ToString();

        int currentHP = GameManager.instance.coreHealth;
        int maxHP = 20; 

        lifeText.text = "Life: " + currentHP;

        lifeBarFill.fillAmount = (float)currentHP / maxHP;
    }
    public void ShowUpgradeUI(string towerName, bool show)
    {
        upgradePanel.SetActive(show);
        towerNameText.text = towerName;
    }
}