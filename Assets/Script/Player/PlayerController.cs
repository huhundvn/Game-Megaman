using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float moveSpeed; // tốc độ chạy
    public float jumpHigh; // độ cao nhảy
    public float hpPlayer, myMana, myScore, myLife; // máu, năng lượng, điểm nhân vật
    public LevelManager levelManager; // quan ly level
    private float hpPlayerTemp;
    
    /*
    * vũ khí
    */
    public GameObject weabon, weabon2, smoke, mana;
    private GameObject tmp;
    public Transform gun, smokeJump;
    public Transform[] checkGround;
    public LayerMask layerMask;
    public float groundRadius;

    public AudioClip soundHurt, soundDie, eatGold;

    /*
     * animator và boxcolider nhân vật 
     */
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private AudioSource myAudioSource;

    /*
     * kiểm tra trang thái nhân vật
     */
    private bool isStart, isRight, isGround, isHurt, canMove, canFight, canJump, isClimb, isDie;
    
    /*
     * vị trí nhân vật
     */
    private float horizontal, vertical;
    private Vector3 myScale;

	// Use this for initialization
	void Start () {
        isRight = true;
        hpPlayerTemp = hpPlayer;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
        isGround = checkIsGround();
        myAnimator.SetBool("start", isStart);
        myAnimator.SetBool("die", isDie);

        if (!isStart)
        {
            start();
        }
        else
        {
            horizontal = ETCInput.GetAxis("Horizontal");
            vertical = ETCInput.GetAxis("Vertical");

            //horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");

            myAnimator.SetBool("isGround", isGround);
            myAnimator.SetFloat("run", Mathf.Abs(horizontal));
            myAnimator.SetBool("climb", isClimb);

            if (canMove)
            {
                move();
            }
            if (canFight)
            {
                fight();
            }
            if (canJump)
            {
                jump();
            }
            if (isClimb)
            {
                climb();
            }
        }
    }

    /*
     * kiểm tra bắt đầu 
     */
     private void start()
    {
        if (isGround && hpPlayer > 0)
        {
            isStart = true;
            canMove = true;
            canJump = true;
            canFight = true;
            isHurt = false;
            myAnimator.SetBool("start", true);
        }   
    }

    /*
     * hàm di chuyển nhân vật 
     */
    private void move()
    {
        if (horizontal > 0)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (horizontal < 0)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        // flip player
        if (horizontal > 0 && !isRight || horizontal < 0 && isRight)
        {
            isRight = !isRight;
            myScale = transform.localScale;
            myScale.x *= -1;
            transform.localScale = myScale;
        }
    }

    private bool checkIsGround()
    {
        if (myRigidbody2D.velocity.y <= 0)
        {
            foreach (Transform point in checkGround)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, layerMask);
                for (int i=0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }    
                }
            }
        }
        return false;
    }

    /*
     * nhân vật nhảy 
     */
    private void jump()
    {
        if (vertical > 0 && isGround)
        {
            isGround = false;
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpHigh);
            Instantiate(smoke, checkGround[0].position, Quaternion.identity);
        } 
    }

    /*
     * nhân vật trèo 
     */
    private void climb()
    {
        myRigidbody2D.gravityScale = 0.0f;
    }

    /*
     * nhân vật bắn 
     */
    private void fight()
    {
        if (ETCInput.GetButton("Fight"))
        {
            myAnimator.SetBool("fight", true);
            myMana += Time.deltaTime;
            if (myMana > 1)
            {
                mana.SetActive(true);
            }
        }

        if (ETCInput.GetButtonUp("Fight"))
        {
            mana.SetActive(false);
            if (isRight)
            {
                if (myMana < 1)
                    Instantiate(weabon, gun.position, Quaternion.identity);
                else
                    Instantiate(weabon2, gun.position, Quaternion.identity);
            } else
            {
                if (myMana < 1)
                    Instantiate(weabon, gun.position, new Quaternion(0, 180, 0, 0));
                else
                    Instantiate(weabon2, gun.position, new Quaternion(0, 180, 0, 0));
            }
            myMana = 0.0f;
            myAnimator.SetBool("fight", false);
        }
    }

    /*
     * nhân vật bị thương 
     */
    private void hurt()
    {
        isHurt = true;
        myAudioSource.PlayOneShot(soundHurt);
        hpPlayer--;
        myAnimator.SetTrigger("isHurt");
        if (hpPlayer <= 0)
        {
            isDie = true;
            die();
        }
    }

    private void die()
    {
        isHurt = false;
        if (myLife > 0)
        {
            myLife--;
            isStart = false;
            canMove = false;
            canJump = false;
            canFight = false;
            myAudioSource.PlayOneShot(soundDie);
            Invoke("respawns", 0.2f);
        }
        else
        {
            levelManager.gameOver();
        }
    }

    /*
     * nhân vật hồi sinh
     */
     private void respawns()
    {
        isDie = false;
        hpPlayer = hpPlayerTemp;
        levelManager.respawnPlayer();
    }

    private void setScore()
    {
        myAudioSource.PlayOneShot(eatGold);
        myScore += 100.0f;
    }

    /*
     * xử lí va chạm 
     */
    void OnCollisionEnter2D (Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "WeabonEnemy": hurt(); break;
            case "Type-2": hurt(); break;
            case "Type-3": hurt(); break;
            case "Type-4": hurt(); break;
            case "Gold": setScore(); break;
            case "FireZone": die(); break;
            case "DieZone": die(); break;
            case "Door":
                myAnimator.SetBool("win", true);
                canMove = false;
                canFight = false;
                canJump = false;
                break;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Type-2": isHurt = false; break;
            case "Type-3": isHurt = false; break;
            case "Type-4": isHurt = false; break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "WeabonEnemy": hurt(); break;
            case "ClimbZone": isClimb = true; break;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "ClimbZone": isClimb = true; break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "ClimbZone":
                isClimb = false;
                myRigidbody2D.gravityScale = 1.0f;
                break;
        }    
    }
}
