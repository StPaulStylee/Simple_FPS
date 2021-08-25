using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

namespace fps.characters {
  public class Enemy : Character
  {
      [Header("Stats")]
      public int ScoreToGive;

      [Header("Movement")]
      public float AttackRange;
      public float YPathOffest;

      private List<Vector3> path;
      [SerializeField]
      private GameObject target;

      private void Start()
      {
          target = FindObjectOfType<Player>().gameObject;
          InvokeRepeating("UpdatePath", 0f, 0.5f);
      }

      private void Update()
      {
          LookAtTarget();
          float dist = Vector3.Distance(transform.position, target.transform.position);
          if (dist <= AttackRange)
          {
              if (weapon.CanShoot())
              { 
                  weapon.Fire();
              }
              return;
          }
          ChaseTarget();
      }

      private void ChaseTarget()
      {
          if (path.Count == 0) { return; }
          // Move towards the closest path
          transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, YPathOffest, 0), MoveSpeed * Time.deltaTime);
          if (transform.position == path[0] + new Vector3(0, YPathOffest, 0))
          {
              path.RemoveAt(0);
          }
      }

      private void LookAtTarget()
      {
          // Look at the target
          Vector3 direction = (target.transform.position - transform.position).normalized;
          float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
          transform.eulerAngles = Vector3.up * angle;
      }

      private void UpdatePath()
      {
          // calculate path to the target
          NavMeshPath navMeshPath = new NavMeshPath();
          NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);
          path = navMeshPath.corners.ToList();
      }

      protected override void Die()
      {
          Destroy(gameObject);
      }
  }
}
