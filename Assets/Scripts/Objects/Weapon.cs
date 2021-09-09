using fps.characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.objectpools {
  public class Weapon : MonoBehaviour {
    public int CurrentAmmo;
    public int MaxAmmo;
    public bool InfiniteAmmo;

    [SerializeField]
    private float refireRate = 0.2f;
    private float fireTimer;
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private GameObject hitParticle;

    private bool isPlayer;

    private void Awake() {
      // Are we attached to the player?
      if (GetComponent<Player>()) {
        isPlayer = true;
      }
      else isPlayer = false;
    }

    private void Update() {
      fireTimer += Time.deltaTime;
    }

    public void Fire() {
      var shot = BulletPool.Instance.Get();
      shot.hitParticle = hitParticle;
      shot.transform.position = muzzle.transform.position;
      shot.transform.rotation = muzzle.transform.rotation;
      shot.gameObject.SetActive(true);
      CurrentAmmo--;
    }

    public bool CanShoot() {
      if (fireTimer >= refireRate) {
        fireTimer = 0f;
        return true;
      }
      return false;
    }
  }
}
