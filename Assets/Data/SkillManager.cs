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
        skillPanel.SetActive(false);


        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats instance is missing!");
            return;
        }
        Debug.Log("Skills");
        Debug.Log(PlayerStats.Instance.reloadTime);
        UpdateUI();
    }

    public void UpgradeBulletSpeed()
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.UpgradeBulletSpeed(2f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void UpgradeMaxHealth()
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.Heal(10f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void UpgradeMaxShieldHealth()
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.HealShield(5f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void DecreaseReloadTime()
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.DecreaseReloadTime(0.5f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void UpgradeSpeed()
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.UpgradeSpeed(0.5f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }


    public void OpenSkill()
    {
        skillPanel.SetActive(true);
        skillButton.gameObject.SetActive(false);
        UpdateUI();
    }

    public void CloseSkill()
    {
        skillPanel.SetActive(false);
        skillButton.gameObject.SetActive(true);
    }

    private void UpdateUI()
    {
        bulletSpeedText.text = PlayerStats.Instance.bulletSpeed.ToString();
        maxHealthText.text = PlayerStats.Instance.playerHealth.ToString();
        maxShieldHealthText.text = PlayerStats.Instance.shieldHealth.ToString();
        reloadTimeText.text = PlayerStats.Instance.reloadTime.ToString();
        speedText.text = PlayerStats.Instance.speed.ToString();
        skillPointsText.text = PlayerStats.Instance.skill.ToString();

        foreach (var item in buttons)
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
