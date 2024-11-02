using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointPickup : MonoBehaviour
{
    public int skillPointValue = 1; // Mennyi skill pontot adjon a játékosnak

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.Instance.AddSkillPoint(skillPointValue); // Hozzáad egy skill pontot a játékoshoz
            Destroy(gameObject); // Skill pont eltávolítása a jelenetbõl
        }
    }
}
