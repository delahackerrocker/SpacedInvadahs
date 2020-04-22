using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum VerticalMovement
{
    FORWARD,
    BACKWARD,
    NOTHING
}

public enum HorizontalMovement
{
    RIGHT,
    LEFT,
    NOTHING
}

public class Player : MonoBehaviour
{
    public GameObject projectileTemplate;

    public Transform projectileSpawnPoint;

    public GameObject particleFX;

    public AudioSource explosionSound;

    protected bool isExploding = false;

    [HideInInspector] public HorizontalMovement horizontalGear = HorizontalMovement.NOTHING;
    [HideInInspector] public VerticalMovement VerticalGear = VerticalMovement.NOTHING;

    protected const float MOVE_MAGNITUDE = 1f;
    protected const float BANK_MAGNITUDE = 20f;

    protected Vector3 moveRightAmount = new Vector3(MOVE_MAGNITUDE, 0, 0);
    protected Vector3 moveLeftAmount = new Vector3(-MOVE_MAGNITUDE, 0, 0);
    protected Vector3 moveForwardAmount = new Vector3(0f, MOVE_MAGNITUDE, 0);
    protected Vector3 moveBackwardAmount = new Vector3(0, -MOVE_MAGNITUDE, 0);

    protected Vector3 bankRightAmount = new Vector3(0, -BANK_MAGNITUDE, 0);
    protected Vector3 bankLeftAmount = new Vector3(0, BANK_MAGNITUDE, 0);

    protected Vector3 newLocation;

    protected float tweenTime = .6f;
    private float timerLength = 0.1f;
    private float targetTime = 0.01f;

    private void Start()
    {
        DOTween.defaultEaseType = Ease.OutBounce;
        newLocation = this.transform.position;
    }

    public void Explode()
    {
        isExploding = true;
        particleFX.SetActive(true);
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
        // Check if can move right

        // Move to the right
        newLocation += moveRightAmount;
    }

    public void MoveLeft()
    {
        // Check if can move left

        // Move to the left
        newLocation += moveLeftAmount;
    }
    public void MoveForward()
    {
        // Check if can move right

        // Move to the right
        newLocation += moveForwardAmount;
    }

    public void MoveBackward()
    {
        // Check if can move left

        // Move to the left
        newLocation += moveBackwardAmount;
    }

    Vector3 bulletRotation = new Vector3(-90f, 0f, 0f);
    public void Shoot()
    {
        Instantiate(projectileTemplate, projectileSpawnPoint.position, Quaternion.Euler(-90f,0f,0f));
    }

    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            TimerEnded();
            targetTime = timerLength;
        }

    }

    public void TimerEnded()
    {
        bool isRotatingRight = false;
        bool isRotatingLeft = false;

        // check if forward or backward
        if (VerticalGear == VerticalMovement.FORWARD)
        {
            MoveForward();
        }
        else if (VerticalGear == VerticalMovement.BACKWARD)
        {
            MoveBackward();
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
        } else if (isRotatingLeft) {
            DORotate(bankLeftAmount);
        } else
        {
            // if we are not moving right or left we rotate back to center
            DORotate(Vector3.zero);
        }
    }
}