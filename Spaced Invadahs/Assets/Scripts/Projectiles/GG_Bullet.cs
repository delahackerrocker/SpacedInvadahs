using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_Bullet : MonoBehaviour
{
    protected bool isAlive = true;
    protected bool isExploding = false;

    public GameObject mesh;

    private GameObject enemy;
    private Transform projectileTarget;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        projectileTarget = enemy.transform;

        target = enemy.transform.position;
        ;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = transform.position + (Vector3.down / 16);

        // Make the missile rotate towards the target
        this.transform.LookAt(target);

        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Projectile OnTriggerEnter");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Projectile struck Enemy");
            other.gameObject.GetComponent<Drone>().Explode();
        }
    }
}
