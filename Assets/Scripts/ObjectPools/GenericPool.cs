using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fps.objectpools {
  public abstract class GenericPool<T> : MonoBehaviour where T : Component {
    [SerializeField]
    private T prefab;

    private Queue<T> objects = new Queue<T>();

    // This is not a good singleton
    public static GenericPool<T> Instance { get; private set; }

    private void Awake() {
      Instance = this;
    }

    public T Get() {
      if (objects.Count == 0) {
        AddObjects(1);
      }
      return objects.Dequeue();
    }

    private void AddObjects(int count) {
      for (int i = 0; i < count; i++) {
        var obj = Instantiate(prefab);
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
      }
    }

    public void ReturnToPool(T objToReturn) {
      objToReturn.gameObject.SetActive(false);
      objects.Enqueue(objToReturn);
    }
  }
}
