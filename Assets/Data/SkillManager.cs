using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI maxShieldHealthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI bulletSpeedText;
    public TextMeshProUGUI reloadTimeText;
    public TextMeshProUGUI skillPointsText;
    public GameObject skillPanel;
    public Button skillButton;


    public Button[] buttons;

    private void Start()
    {
        skillPanel.SetActive(false); // Kezdetben a képesség panel rejtett


        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats instance is missing!");
            return;
        }
        Debug.Log("Skills"); // Napló üzenet a teszteléshez
        Debug.Log(PlayerStats.Instance.reloadTime); // Jelenlegi újratöltési idő naplózása
        UpdateUI();
    }

    public void UpgradeBulletSpeed() // Lövedék sebességének fejlesztése
    {
        if (PlayerStats.Instance.skill > 0) // Ellenőrzi, van-e elég képességpont
        {
            PlayerStats.Instance.UpgradeBulletSpeed(2f); // Növeli a lövedék sebességét
            PlayerStats.Instance.skill--; // Csökkenti a képességpontok számát
            UpdateUI();
        }
    }

    public void UpgradeMaxHealth() // Maximális életerő fejlesztése
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.Heal(10f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void UpgradeMaxShieldHealth() // Pajzs maximális életerejének fejlesztése
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.HealShield(5f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void DecreaseReloadTime() // Újratöltési idő csökkentése
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.DecreaseReloadTime(0.5f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void UpgradeSpeed() // Játékos sebességének növelése
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.UpgradeSpeed(0.5f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }


    public void OpenSkill() // Képesség panel megnyitása
    {
        skillPanel.SetActive(true);
        skillButton.gameObject.SetActive(false);
        UpdateUI();
    }

    public void CloseSkill() // Képesség panel bezárása
    {
        skillPanel.SetActive(false);
        skillButton.gameObject.SetActive(true);
    }

    private void UpdateUI()  // UI elemek frissítése a játékos aktuális statisztikáinak megfelelően
    {
        bulletSpeedText.text = PlayerStats.Instance.bulletSpeed.ToString();
        maxHealthText.text = PlayerStats.Instance.playerHealth.ToString();
        maxShieldHealthText.text = PlayerStats.Instance.shieldHealth.ToString();
        reloadTimeText.text = PlayerStats.Instance.reloadTime.ToString();
        speedText.text = PlayerStats.Instance.speed.ToString();
        skillPointsText.text = PlayerStats.Instance.skill.ToString();

        foreach (var item in buttons) // A gombok aktiválása vagy letiltása attól függően, hogy van-e elég képességpont
        {
            if(PlayerStats.Instance.skill > 0)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
