using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BallController : MonoBehaviour
{
    private UIManager _UIManager;
    
    public float moveSpeed = 5f; // Adjust the speed as needed
    private Rigidbody rb;
    public bool isMoving = false;
    public Vector3 originalScale;
    public Vector3 diamondMoveLocation;
    public int diamondCount;
    
    public TextMeshProUGUI diamondTxt;
    

    void Start()
    {
        _UIManager = GetComponent<UIManager>();
        rb = GetComponent<Rigidbody>();
        
        originalScale = gameObject.transform.localScale;
        
        diamondMoveLocation = new Vector3(9.5f, 0.7f, 17f);
    }

    void FixedUpdate()
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

        if (horizontalInput == 0 || verticalInput == 0)
        {
            Vector3 moveDirection = new Vector3(horizontalInput,0, verticalInput).normalized;
            Vector3 moveVelocity = moveDirection * moveSpeed;
            
            rb.velocity = moveVelocity;
            if (moveDirection != Vector3.zero)
            {
                isMoving = true;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Diamond"))
        {
            SoundManager.instance.PlaySoundEffect(0);
            diamondCount++;
            
            collision.gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.1f)
                .OnComplete(()=>collision.gameObject.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.9f));
            
            collision.gameObject.transform.DOMove(diamondMoveLocation, 1f)
                .OnComplete(()=>diamondTxt.text = diamondCount.ToString());

            collision.gameObject.transform.DOJump(diamondMoveLocation, 0, 0, 1f)
                .OnComplete(()=>collision.gameObject.SetActive(false));
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            SoundManager.instance.PlaySoundEffect(1);
            //_UIManager.LevelFinish();
            enabled = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //rb.velocity = Vector3.zero;
            gameObject.transform.DOScale(originalScale, 0.4f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            rb.velocity = Vector3.zero;
            
            isMoving = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 decreaseScale = new Vector3(.15f, .15f, .15f);
            gameObject.transform.DOScale(originalScale-decreaseScale, 0.4f);
            
            isMoving = true;
        }
    }
}
