using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointPickup : MonoBehaviour
{
    public int skillPointValue = 1; // Mennyi skill pontot adjon a jatekosnak

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.Instance.AddSkillPoint(skillPointValue); // Hozzaad egy skill pontot a jatekoshoz
            Destroy(gameObject); // Skill pont eltavolitasa a jelenetbol
        }
    }
}
