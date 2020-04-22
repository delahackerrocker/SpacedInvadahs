using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Bullet : MonoBehaviour
{
    protected bool isAlive = true;
    protected bool isExploding = false;

    public GameObject mesh;

    public AudioSource explosionSound;

    private GameObject enemy;
    private Transform projectileTarget;
    private Vector3 target;

    public GameObject particleFX;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Player");
        projectileTarget = enemy.transform;

        target = enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.position = transform.position + (Vector3.down / 16);

            if (isExploding)
            {
                if (mesh.activeSelf) { explosionSound.Play(); }
                mesh.SetActive(false);
                if (particleFX != null)
                {
                    if (particleFX.GetComponent<ParticleSystem>().IsAlive())
                    {
                        // it's still animating the explosion
                    }
                    else
                    {
                        isExploding = false;
                        particleFX.GetComponent<ParticleSystem>().Clear();
                        Destroy(this.gameObject);
                        Destroy(this);
                    }
                }
            }
            else
            {
                // the Drone is alive and not exploding
                /*
                targetTime -= Time.deltaTime;

                if (targetTime <= 0.0f)
                {
                    TimerEnded();
                    targetTime = timerLength;
                }
                */
            }
        }
        else
        {
            Destroy(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("BG_Bullet OnTriggerEnter: "+other.name);
        if (other.CompareTag("Player"))
        {
            isExploding = true;

            // Play the particle effect for the bullet
            particleFX.SetActive(true);

            other.gameObject.GetComponent<Player>().Explode();
        }
    }
}