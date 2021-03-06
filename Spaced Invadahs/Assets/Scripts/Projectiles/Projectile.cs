﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 4;

    private GameObject enemy;
    private Transform projectileTarget;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Player");
        projectileTarget = enemy.transform;

        target = enemy.transform.position;
;   
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = transform.position + (Vector3.down/16);

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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile struck Enemy");
            other.gameObject.GetComponent<Assault>().Explode();
        }
    }
}
