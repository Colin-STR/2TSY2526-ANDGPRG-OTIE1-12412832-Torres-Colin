using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI towerInfoText;
    public TextMeshProUGUI costText;

    private Tower selectedTower;

    public void Open(Tower tower)
    {
        selectedTower = tower;

        towerNameText.text = tower.gameObject.name.Replace("(Clone)", "");
        UpdateInfoDisplay();

        transform.position = Camera.main.WorldToScreenPoint(tower.transform.position + Vector3.up * 2f);
        gameObject.SetActive(true);
    }

    void UpdateInfoDisplay()
    {
        float dmg = selectedTower.CurrentLevel.damage;
        float range = selectedTower.CurrentLevel.range;
        float rate = selectedTower.CurrentLevel.fireRate;

        towerInfoText.text = $"Damage: {dmg}\nRange: {range}\nFire Rate: {rate}";

        int cost = selectedTower.GetUpgradeCost();
        costText.text = (cost == -1) ? "MAX LEVEL" : $"Upgrade: ${cost}";
    }

    public void OnUpgradeButtonClicked()
    {
        int cost = selectedTower.GetUpgradeCost();
        if (cost != -1 && GameManager.instance.TrySpendGold(cost))
        {
            selectedTower.Upgrade();
            UpdateInfoDisplay();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}