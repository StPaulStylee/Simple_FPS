﻿using fps.characters;
using fps.objectpools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.objects {
  public class Bullet : MonoBehaviour {
    public float MoveSpeed;
    public int Damage;
    private float lifetime;
    [SerializeField]
    private float maxLifetime = 5f;

    private void OnEnable() {
      lifetime = 0f;
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
        other.GetComponent<Player>().TakeDamge(Damage);
        return;
      }
      if (other.CompareTag("Enemy")) {
        other.GetComponent<Enemy>().TakeDamge(Damage);
      }
      BulletPool.Instance.ReturnToPool(this);
    }

    private void Update() {
      transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
      lifetime += Time.deltaTime;
      if (lifetime >= maxLifetime) {
        BulletPool.Instance.ReturnToPool(this);
      }
    }
  }
}
