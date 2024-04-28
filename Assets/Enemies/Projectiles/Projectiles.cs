using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Vector3 direction;
    public int damage;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        direction = (PlayerController.instance.transform.position - transform.position).normalized;
        StartCoroutine("DeathTimer");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * direction);
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{other.gameObject.tag} on Layer {other.gameObject.layer}");
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.TakeDamage(damage, Damageable.EDamage.Ranged);
            Destroy(gameObject);
        }
    }
}
