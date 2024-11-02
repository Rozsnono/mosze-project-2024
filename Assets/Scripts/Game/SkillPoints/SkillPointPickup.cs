using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointPickup : MonoBehaviour
{
    public int skillPointValue = 1; // Mennyi skill pontot adjon a j�t�kosnak

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.Instance.AddSkillPoint(skillPointValue); // Hozz�ad egy skill pontot a j�t�koshoz
            Destroy(gameObject); // Skill pont elt�vol�t�sa a jelenetb�l
        }
    }
}
