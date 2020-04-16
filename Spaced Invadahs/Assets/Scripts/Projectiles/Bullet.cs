using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 4;

    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Projectile OnTriggerEnter");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Projectile struck Enemy");
            other.gameObject.GetComponent<Drone>().Explode();
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
