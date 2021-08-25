using fps.objectpools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.characters {
  public abstract class Character : MonoBehaviour
  {
      [Header("Stats")]
      public int CurrentHp;
      public int MaxHp;

      [Header("Movement")]
      public float MoveSpeed; // Movement Speed in units per second

      protected Weapon weapon;

      protected virtual void Awake()
      {
          weapon = GetComponentInChildren<Weapon>();
      }

      public void TakeDamge(int damage)
      {
          CurrentHp -= damage;
          if (CurrentHp <= 0)
          {
              Die();
          }
      }

      protected abstract void Die();
  }
}
