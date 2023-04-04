using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject box;

    void Start()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
    }

    public void Die()
    {
        StartCoroutine(SetInactive());
        GetComponent<Animator>().enabled = false;
        box.SetActive(false);
        SetRigidbodyState(false);
        SetColliderState(true);
    }

    void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.isKinematic = state;
    }

    void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
            collider.enabled = state;

        GetComponent<Collider>().enabled = !state;
        box.GetComponent<Collider>().enabled = true;
    }

    IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}