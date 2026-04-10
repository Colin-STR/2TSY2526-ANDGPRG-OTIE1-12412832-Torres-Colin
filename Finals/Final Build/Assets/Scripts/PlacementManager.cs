using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    [Header("Tower Prefabs")]
    public GameObject cannonPrefab;
    public GameObject arrowPrefab;

    [Header("UI References")]
    public UpgradeUI upgradeUI;

    private GameObject towerToBuild;
    private int towerCost;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Tower"))
                {
                    Tower tower = hit.collider.GetComponent<Tower>();
                    if (tower != null && upgradeUI != null)
                    {
                        upgradeUI.Open(tower);
                        towerToBuild = null;
                    }
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    if (towerToBuild != null)
                    {
                        BuildTower(hit.point);
                    }

                    if (upgradeUI != null) upgradeUI.Hide();
                }
                else
                {
                    if (upgradeUI != null) upgradeUI.Hide();
                }
            }
        }
    }

    public void SelectCannon()
    {
        towerToBuild = cannonPrefab;
        towerCost = 100;
        if (upgradeUI != null) upgradeUI.Hide();
    }

    public void SelectArrowTower()
    {
        towerToBuild = arrowPrefab;
        towerCost = 50;
        if (upgradeUI != null) upgradeUI.Hide();
    }

    void BuildTower(Vector3 position)
    {
        if (GameManager.instance.TrySpendGold(towerCost))
        {
            Instantiate(towerToBuild, position, Quaternion.identity);
        }
    }
}