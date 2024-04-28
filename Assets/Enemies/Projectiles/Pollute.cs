using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pollute : MonoBehaviour
{
    public float scale;
    public float activateDelay;
    public float duration;
    public bool dealDamage;
    public float damageDelay = .2f;
    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        StartCoroutine("Activate");
    }

    IEnumerator Activate()
    {
        WaitForSeconds wait = new WaitForSeconds(activateDelay);
        yield return wait;
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine("Deactivate");
    }

    IEnumerator Deactivate()
    {
        WaitForSeconds wait = new WaitForSeconds(duration);
        yield return wait;
        Destroy(gameObject);
    }
    IEnumerator DamageDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(damageDelay);
        yield return wait;
        dealDamage = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log($"Interacting with {other.gameObject.tag}");
        if (other.gameObject.CompareTag("Player"))
        {
            if (dealDamage)
            {
                dealDamage = false;
                PlayerController.instance.TakeDamage(1, Damageable.EDamage.Magic);
                StartCoroutine("DamageDelay");
            }
        }
    }
}
