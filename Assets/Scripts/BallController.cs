using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BallController : MonoBehaviour
{
    public  UIManager _UIManager;
    public LevelManager levelManager;
    
    public float moveSpeed = 5f; // Adjust the speed as needed
    private Rigidbody rb;
    public bool isMoving = false;
    public Vector3 originalScale;
    public Vector3 diamondMoveLocation;

    public float pressTimer;
    

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        _UIManager = FindObjectOfType<UIManager>();
        rb = GetComponent<Rigidbody>();
        
        originalScale = gameObject.transform.localScale;
        
        diamondMoveLocation = new Vector3(9.5f, 0.7f, 17f);
        pressTimer = 0;
    }

    void Update()
    {
        //SkipLevel();
        if (!isMoving && pressTimer <= 0.20f)
        {
            pressTimer += Time.deltaTime;
            isMoving = false;
        }
    }

    private void SkipLevel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int unlockedLevel = levelManager.levelNumber + 1;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.Save();

            _UIManager.LevelFinish();
        }
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            Move();
        }
        
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if ((horizontalInput == 0 || verticalInput == 0) && pressTimer >= 0.20f)
        {
            Vector3 moveDirection = new Vector3(horizontalInput,0, verticalInput).normalized;
            Vector3 moveVelocity = moveDirection * moveSpeed;
            
            rb.velocity = moveVelocity;
            if (moveDirection != Vector3.zero)
            {
                pressTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Diamond"))
        {
            SoundManager.instance.PlaySoundEffect(0);
            //diamondCount++;
            
            collider.gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.1f)
                .OnComplete(()=>collider.gameObject.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.9f));
            
            collider.gameObject.transform.DOMove(diamondMoveLocation, 1f);
        
        }

        if (collider.gameObject.CompareTag("Finish"))
        {
            SoundManager.instance.PlaySoundEffect(1);
            //_UIManager.LevelFinish();
            enabled = false;

            int unlockedLevel = levelManager.levelNumber + 1;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.Save();

            _UIManager.LevelFinish();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //rb.velocity = Vector3.zero;
            
            gameObject.transform.DOScale(originalScale, 0.4f);
            isMoving = false;
            pressTimer = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isMoving)
        {
            
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isMoving = true;
            Vector3 decreaseScale = new Vector3(.15f, .15f, .15f);
            gameObject.transform.DOScale(originalScale-decreaseScale, 0.4f);
        }
    }
}
