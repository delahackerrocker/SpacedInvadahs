using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drone : MonoBehaviour
{
    protected bool isAlive = true;
    protected bool isExploding = false;

    public GameObject mesh;

    public GameObject particleFX;

    public AudioSource explosionSound;

    public GameObject bulletTemplate;

    [HideInInspector] public HorizontalMovement horizontalGear = HorizontalMovement.NOTHING;
    [HideInInspector] public VerticalMovement VerticalGear = VerticalMovement.NOTHING;

    protected const float MOVE_MAGNITUDE = .5f;
    protected const float BANK_MAGNITUDE = 20f;

    protected Vector3 moveRightAmount = new Vector3(MOVE_MAGNITUDE, 0, 0);
    protected Vector3 moveLeftAmount = new Vector3(-MOVE_MAGNITUDE, 0, 0);

    protected Vector3 bankRightAmount = new Vector3(0, -BANK_MAGNITUDE, 0);
    protected Vector3 bankLeftAmount = new Vector3(0, BANK_MAGNITUDE, 0);

    protected Vector3 tooFarLeft = new Vector3(-8.04f, -3.23f, 0f);
    protected Vector3 tooFarRight = new Vector3(8.04f, -3.23f, 0f);

    protected Vector3 newLocation;

    protected float tweenTime = .6f;
    private float timerLength = 0.1f;
    private float targetTime = 0.01f;

    protected GameObject thePlayer;

    protected Vector3 targetLocation;

    private void Start()
    {
        if (particleFX != null) particleFX.SetActive(false);

        thePlayer = GameObject.FindGameObjectWithTag("Player");

        RandomizeDirection();

        DOTween.defaultEaseType = Ease.OutBounce;
        newLocation = this.transform.position;
    }

    public void Explode()
    {
        isExploding = true;
        if (particleFX != null) particleFX.SetActive(true);
    }

    public void RandomizeDirection()
    {
        float randomizeDirection = Random.Range(0f, 1f);
        if (randomizeDirection > .51)
        {
            horizontalGear = HorizontalMovement.RIGHT;
        }
        else
        {
            horizontalGear = HorizontalMovement.LEFT;
        }
    }

    public void DOMove(Vector3 newLocation)
    {
        this.transform.DOMove(newLocation, tweenTime);
    }

    public void DORotate(Vector3 newRotation)
    {
        this.transform.DORotate(newRotation, tweenTime);
    }

    public void MoveRight()
    {
        // Move to the right
        newLocation += moveRightAmount;

        // Check if can move right
        if (this.transform.position.x > tooFarRight.x)
        {
            horizontalGear = HorizontalMovement.LEFT;
        }
    }

    public void MoveLeft()
    {
        // Move to the left
        newLocation += moveLeftAmount;

        // Check if Goomba can move left
        if (this.transform.position.x < tooFarLeft.x)
        {
            horizontalGear = HorizontalMovement.RIGHT;
        }
    }

    public void Shoot()
    {
        Instantiate(bulletTemplate, this.gameObject.transform.position, Quaternion.Euler(90f, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
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
                targetTime -= Time.deltaTime;

                if (targetTime <= 0.0f)
                {
                    TimerEnded();
                    targetTime = timerLength;
                }

                int decideToShoot = Random.Range(0, 100);
                //Debug.Log("decideToShoot: " + decideToShoot);
                if (decideToShoot > 95)
                {
                    // shoot a bullet
                    Shoot();
                }
            }
        } else
        {
            Destroy(this);
        }
    }

    public void TimerEnded()
    {
        if (isAlive)
        {
            bool isRotatingRight = false;
            bool isRotatingLeft = false;

            // check if forward or backward
            if (VerticalGear == VerticalMovement.FORWARD)
            {
                //MoveForward();
            }
            else if (VerticalGear == VerticalMovement.BACKWARD)
            {
                //MoveBackward();
            }

            // check if left or right
            if (horizontalGear == HorizontalMovement.LEFT)
            {
                MoveLeft();
                isRotatingLeft = true;
            }
            else if (horizontalGear == HorizontalMovement.RIGHT)
            {
                MoveRight();
                isRotatingRight = true;
            }

            DOMove(newLocation);

            // If we are moving right or left we will bank
            if (isRotatingRight)
            {
                DORotate(bankRightAmount);
            }
            else if (isRotatingLeft)
            {
                DORotate(bankLeftAmount);
            }
            else
            {
                // if we are not moving right or left we rotate back to center
                DORotate(Vector3.zero);
            }

            float randomizeDirectionChange = Random.Range(0f, 1f);
            if (randomizeDirectionChange > .91)
            {
                RandomizeDirection();
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
