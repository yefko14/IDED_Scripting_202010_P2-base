using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRefactor : MonoBehaviour
{
    public const int PLAYER_LIVES = 3;

    private const float PLAYER_RADIUS = 0.4F;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1F;

    private float hVal;

    #region Bullet

    [Header("Bullet")]
    [SerializeField]
    private PoolBullet bullet;

    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private float bulletSpeed = 3F;

    #endregion Bullet


    #region BoundsReferences

    private float referencePointComponent;
    private float leftCameraBound;
    private float rightCameraBound;

    #endregion BoundsReferences

    #region StatsProperties

    public int Score { get; set; }
    public int Lives { get; set; }

    #endregion StatsProperties

    #region MovementProperties

    public bool ShouldMove
    {
        get =>
            (hVal != 0F && InsideCamera) ||
            (hVal > 0F && ReachedLeftBound) ||
            (hVal < 0F && ReachedRightBound);
    }

    private bool InsideCamera
    {
        get => !ReachedRightBound && !ReachedLeftBound;
    }

    private bool ReachedRightBound { get => referencePointComponent >= rightCameraBound; }
    private bool ReachedLeftBound { get => referencePointComponent <= leftCameraBound; }

    private bool CanShoot { get => bulletSpawnPoint != null && bullet != null; }

    #endregion MovementProperties

    public delegate void OnPlayerDied();
    public event OnPlayerDied onPlayerDied;

    public delegate void OnPlayerHit(int delta);
    public event OnPlayerHit onPlayerHit;

    public delegate void OnPlayerScoreChanged(int delta);
    public event OnPlayerScoreChanged onPlayerScoreChanged;

    void Start()
    {
        leftCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            0F, 0F, 0F)).x + PLAYER_RADIUS;

        rightCameraBound = Camera.main.ViewportToWorldPoint(new Vector3(
            1F, 0F, 0F)).x - PLAYER_RADIUS;
        
        Lives = PLAYER_LIVES;
    }

    void Update()
    {
        
            hVal = Input.GetAxis("Horizontal");

            if (ShouldMove)
            {
                transform.Translate(transform.right * hVal * moveSpeed * Time.deltaTime);
                referencePointComponent = transform.position.x;
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                && CanShoot)
            {
                GameObject clone = bullet.GetItem(bulletSpawnPoint.position);
                clone.GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed, ForceMode.Impulse);
        }
        
    }

    public void PlusPoints(int delta)
    {
        Score += delta;
        if (onPlayerScoreChanged != null)
        {
            onPlayerScoreChanged(delta);
        }
    }

    public void ApplyDamage(int delta)
    {
        Lives -= delta;

        if (Lives > 0)
        {
            if (onPlayerHit != null)
            {
                onPlayerHit(delta);
            }
        }
        if (Lives <= 0)
        {
            if (onPlayerHit != null)
            {
                onPlayerHit(delta);
            }
            if (onPlayerDied != null)
            {
                onPlayerDied();
                this.enabled = false;
                gameObject.SetActive(false); 
            }
        }
    }
}
