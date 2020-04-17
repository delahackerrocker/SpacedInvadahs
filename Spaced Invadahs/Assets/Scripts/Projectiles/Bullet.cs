using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        Debug.Log("bullet onTriggerEnter struck "+ other.name);
        if (other.CompareTag("Enemy"))
        {
            print("bullet struck an enemy");
            Debug.Log("Struck Enemey");
            other.gameObject.GetComponent<Drone>().Explode();
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
