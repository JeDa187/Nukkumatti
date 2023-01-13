using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D rb2D;

    
    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

 
    public Image filler;
    public float counter;
    public float maxCounter;

    //Doublejump
    public int extraJumps;

    int jumpCount = 0;
    float jumpCoolDown;

    //Parachute
    public float glidingSpeed;
    public float initialGravityScale;


    void Start()
    {
        // T‰m‰ on sama asia kuin raahaisi inspectorissa
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        GameManager.manager.historyHealth = GameManager.manager.health;
        GameManager.manager.historyPreviousHealth = GameManager.manager.previousHealth;
        GameManager.manager.historyMaxHealth = GameManager.manager.maxHealth;

    }


    void Update()
    {
        //Parachute
        if (Input.GetKey(KeyCode.P) & !grounded && rb2D.velocity.y <= 0)
        {
            rb2D.gravityScale = 0;
            rb2D.velocity = new Vector2(rb2D.velocity.x, y:-glidingSpeed);
            animator.SetBool("Parachute", true);
        }
        else
        {
            rb2D.gravityScale = initialGravityScale;
            animator.SetBool("Parachute", false);
        }

        // Ground testi, eli ollaanko kosketuksissa maahan vai ei.
        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
            animator.SetBool("Jump", false);
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.5f;
            
        }
        else
        {
            grounded = false;
            animator.SetBool("Jump", true);

        }

        if (Time.time < jumpCoolDown)
        {
            grounded = true;
        }


        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            // Meill‰ on a tai d pohjassa
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            animator.SetBool("Walk", true); 
        }
        else
        {
            // T‰m‰ ajetaan kun ollaan pys‰hdyksiss‰
            animator.SetBool("Walk", false);
        }

        // Hyppy
        if (Input.GetButtonDown("Jump") && grounded && jumpCount < extraJumps)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            jumpCount++;
            
        }

        // t‰m‰ luo counterille laskurin, joka kasvaa maxCounteriin ja aloittaa uudestaan 0:sta. 
        if(counter > maxCounter)
        {
            GameManager.manager.previousHealth = GameManager.manager.health;
            counter = 0;
            
        }
        else
        {
            counter += Time.deltaTime;
        }


        filler.fillAmount = Mathf.Lerp(GameManager.manager.previousHealth / GameManager.manager.maxHealth, GameManager.manager.health / GameManager.manager.maxHealth, counter / maxCounter);


        if(gameObject.transform.position.y < -50)
        {
            Die();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            // Ollaan osuttu ansaan -> V‰hennet‰‰n healthia.
            TakeDamage(40);
        }

        

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AddHealth"))
        {
            // Pelaaja on koskettanut syd‰nt‰. Tuhotaan syd‰n ja lis‰t‰‰n 30 healthia. 
            Destroy(collision.gameObject); // Tuhotaan syd‰n
            Heal(30);
        }

        if (collision.CompareTag("AddMaxHealth"))
        {
            Destroy(collision.gameObject);
            AddMaxHealth(40);
        }

        if (collision.CompareTag("LevelEnd"))
        {
            GameManager.manager.previousLevel = GameManager.manager.currentLevel;
            SceneManager.LoadScene("Map");
        }
    }


    void AddMaxHealth(float addMaxHealthAmount)
    {
        GameManager.manager.maxHealth += addMaxHealthAmount;
    }

    void Heal(float healAmount)
    {
        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0;
        GameManager.manager.health += healAmount;
        GameManager.manager.health = Mathf.Clamp(GameManager.manager.health, 0, GameManager.manager.maxHealth);
    }

    void TakeDamage(float dmg)
    {

        GameManager.manager.previousHealth = filler.fillAmount * GameManager.manager.maxHealth;
        counter = 0; 
        GameManager.manager.health -= dmg; // T‰m‰ v‰hent‰‰ damage:n verran health arvosta. health = health - dmg;

        if(GameManager.manager.health < 0)
        {

            Die();

        }

    }

    public void Die()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel;
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.previousHealth = GameManager.manager.historyPreviousHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        SceneManager.LoadScene("Map");

    }


}
