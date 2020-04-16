using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 4;

    private GameObject[] enemies;
    private Transform projectileTarget;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int chooseRandomEnemy = Random.Range(0, enemies.Length);
        projectileTarget = enemies[chooseRandomEnemy].transform;

        target = new Vector3(projectileTarget.position.x, projectileTarget.position.y, projectileTarget.position.z);   
    }

    // Update is called once per frame
    void Update()
    {
        // Heat Seeking Mode Stuff
        target = new Vector3(projectileTarget.position.x, projectileTarget.position.y, projectileTarget.position.z);

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Make the missile rotate towards the target
        this.transform.LookAt(target);

        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            DestroyProjectile();
        }
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
