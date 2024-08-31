using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform strecthTransform = null;
    [SerializeField] float stretchDamp = 0f;
    [SerializeField] float stretchStrength = 0f;
    [SerializeField] float touchRadius = 0f;
    [SerializeField] float moveSpeed = 0f;
    [SerializeField] float blownForce = 0f;
    [SerializeField] float bounceForce = 0f;
    [SerializeField] float limitFallSpeed = 1f;
    public int tutorial;


    //References for <RopeDrawer>
    RopeDrawer rope;
    [HideInInspector] public Transform target; //Using transform to avoid bug. If it's destroyed, it'll nullify
    [HideInInspector] public bool targetFound;

    //Player's compoenents
    Rigidbody2D rb;
    Camera mainCam;

    //Sound Effects
    Sound targetDestroyedSound,
        bombDestroyedSound,
        heartDestroyedSound;

    //Outside player components
    ParticleManager particle;
    CameraShaker shaker;
    Score score;
    Energy energy;
    Life life;

    //Target Tags
    [SerializeField] List<string> targetTags = new List<string>();

    void Awake()
    {
        tutorial = PlayerPrefs.GetInt("Tutorial", 0);

        targetFound = false;
        mainCam = Camera.main;

        rb = GetComponent<Rigidbody2D>();
        rope = GetComponent<RopeDrawer>();

        particle = FindObjectOfType<ParticleManager>();
        shaker = FindObjectOfType<CameraShaker>();
        score = FindObjectOfType<Score>();
        energy = FindObjectOfType<Energy>();
        life = FindObjectOfType<Life>();

        targetDestroyedSound = FindObjectOfType<AudioManager>().GetClip("Target Destroyed");
        bombDestroyedSound= FindObjectOfType<AudioManager>().GetClip("Bomb Destroyed");
        heartDestroyedSound = FindObjectOfType<AudioManager>().GetClip("Heart Destroyed");
    }

    void Start()
    {
        if(tutorial == 0)
        {
            rb.gravityScale = 0f;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !targetFound && !rope.reachingTarget && energy.ratio > 0f && tutorial == 1)
        {
            FindTarget();
        }
    }

    void FixedUpdate()
    {
        PlayerLookDirection();
        if (targetFound && target != null)
        {
            LookAtTarget();
            MoveToTarget();
        }
        else
        {
            //To avoid bug. When targetFound is true, but the target doesn't exists
            targetFound = false;
        }

        /*
        if(rb.velocity.y < -limitFallSpeed && !targetFound)
        {
            LimitFallSpeed();
        }*/

        StretchPlayer();
    }

    void LimitFallSpeed()
    {
        Vector2 desiredVelocity = new Vector2(rb.velocity.x, -limitFallSpeed);
        rb.velocity = desiredVelocity;
    }

    void FindTarget()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

        Collider2D[] collider = Physics2D.OverlapCircleAll(mousePos, touchRadius);

        if(collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                string tag = collider[i].tag;
                if (targetTags.Contains(tag))
                {
                    energy.started = true;

                    targetFound = false;
                    target = collider[i].transform;

                    particle.TapEffect(target.position);
                    StopCoroutine(rope.ShootRope());
                    StartCoroutine(rope.ShootRope());

                    break;
                }
            }
        }
    }
    void PlayerLookDirection()
    {
        transform.right = rb.velocity;
    }

    void LookAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void MoveToTarget()
    {
        rb.velocity = transform.right * moveSpeed * Time.fixedDeltaTime;
    }

    void StretchPlayer()
    {
        float x = rb.velocity.x;
        float y = rb.velocity.y;

        if(x < 0)
        {
            x *= -1;
        }
        if(y< 0)
        {
            y *= -1;
        }
        float stretch = stretchStrength * (x + y);
        Vector3 stretchDir = new Vector3(stretch, -stretch, 0f);

        strecthTransform.localScale += stretchDir;
        strecthTransform.localScale = Vector3.Lerp(strecthTransform.localScale, Vector3.one, stretchDamp);
    }

    /*void FlipPlayer()
    {
        //Makes player look left or right
        float z = moveDirector.localRotation.z;
        if (z <= -.5f || z > .5f)
        {
            //Looks left
            sprite.flipX = true;
        }
        else
        {
            //Looks right
            sprite.flipX = false;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.tag;
        if (targetTags.Contains(tag))
        {
            targetFound = false;
            Destroy(collider.gameObject);
            
            //For some reasons, the heart object don't get destroyed at fucking all.
            //There's no error either
            //And other target objects work just fine
            //So, I don't fucking know what in the fucking hell is the problem.

            //Particle
            particle.TargetDestroyed(tag, collider.transform.position);
            particle.PointAddedEffect(tag, collider.transform.position);

            //Camera Shake
            StartCoroutine(shaker.CameraShake());
            
            //Energy
            energy.RefillEnergy();

            //Bounce
            rb.velocity = Vector2.up * blownForce + -rb.velocity * bounceForce;

            //Score point
            if (tag == "Bomb")
            {
                score.AddScore(-50);
                life.AddLife(-1);
                bombDestroyedSound.Play();
            }else if(tag == "Life")
            {
                life.AddLife(1);
                heartDestroyedSound.Play();
            }
            else 
            { 
                score.AddScore(10); 
                targetDestroyedSound.Play();
            }
            score.AddTargetHits();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Ground")
        {
            energy.started = false;
            energy.RefillEnergy();
        }
    }
}
