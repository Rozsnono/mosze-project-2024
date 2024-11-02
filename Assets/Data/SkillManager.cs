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
    public Button skilLButton;


    public Button[] buttons;

    private void Start()
    {
        skillPanel.SetActive(false);

        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats instance is missing!");
            return;
        }
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
            PlayerStats.Instance.UpgradeMaxHealth(10f);
            PlayerStats.Instance.skill--;
            UpdateUI();
        }
    }

    public void UpgradeMaxShieldHealth()
    {
        if (PlayerStats.Instance.skill > 0)
        {
            PlayerStats.Instance.UpgradeMaxShieldHealth(5f);
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
        if (PlayerStats.Instance.skill > 0)
        {
            skillPanel.SetActive(true);
            skilLButton.gameObject.SetActive(false);
            UpdateUI();
        }
    }

    public void CloseSkill()
    {
        skillPanel.SetActive(false);
        skilLButton.gameObject.SetActive(true);
    }

    private void UpdateUI()
    {
        bulletSpeedText.text = PlayerStats.Instance.bulletSpeed.ToString();
        maxHealthText.text = PlayerStats.Instance.maxHealth.ToString();
        maxShieldHealthText.text = PlayerStats.Instance.maxShieldHealth.ToString();
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
