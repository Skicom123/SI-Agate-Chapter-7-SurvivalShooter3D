using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        //mendapatkan referensi komponen
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if (isSinking)
        {
            //memindahkan object ke bawah
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;

        //ganti posisi particle serta memPlay
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        //set isdead
        isDead = true;
 
        //SetCapcollider ke trigger
        capsuleCollider.isTrigger = true;
 
        //trigger play animation Dead
        anim.SetTrigger ("Dead");
 
        //Play Sound Dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        //disable Navmesh Component
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        //Set rigisbody ke kimematic
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
