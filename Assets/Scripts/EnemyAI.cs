using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform attackPoint;
    public GameObject projectilePrefab;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    //public TMP_Text stateText;
    public Animator anim;

    [Header("AI Stats")]
    public float health = 50f;
    public float damage = 5f;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    private bool isWalkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttack;
    private bool hasAlreadyAttacked;

    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool isPlayerInSightRange, isPlayerInAttackRange, canSeePlayer;
    private BehaviourState behaviourState;
    public enum BehaviourState {
        patroling,
        investigating,
        chasing,
        attacking
    }

    private Vector3 lastPlayerPosition;
    private bool isInvestigating;

    private void Awake() {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update() {

        //Checking for sight and attack range
        isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        canSeePlayer = Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, sightRange, whatIsPlayer);
        print("Canseeplayer: " + canSeePlayer);

        if (!isPlayerInSightRange && !isPlayerInAttackRange && !canSeePlayer && !isInvestigating) Patroling();
        if (isPlayerInSightRange && !isPlayerInAttackRange && canSeePlayer) {
            lastPlayerPosition = player.transform.position;
            ChasePlayer();
        }
        if (!isPlayerInSightRange && !isPlayerInAttackRange && !canSeePlayer && isInvestigating) Investigate();
        if (isPlayerInSightRange && isPlayerInAttackRange) AttackPlayer();

        if (anim != null) HandleAnimations();
    }

    private void Patroling() {
        behaviourState = BehaviourState.patroling;

        //stateText.text = "Patroling";
        //stateText.color = Color.green;

        if (!isWalkPointSet) SearchWalkPoint();
        if (isWalkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkpoint = transform.position - walkPoint;

        if (distanceToWalkpoint.magnitude < 1f) {
            isWalkPointSet = false;
        }
    }

    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            isWalkPointSet = true;
        }
    }

    private void ChasePlayer() {
        behaviourState = BehaviourState.chasing;

        agent.SetDestination(lastPlayerPosition);

        //stateText.text = "Chasing";
        //stateText.color = Color.yellow;
    }

    private void Investigate() {
        behaviourState = BehaviourState.investigating;
        agent.SetDestination(lastPlayerPosition);

        Vector3 distanceToWalkpoint = transform.position - lastPlayerPosition;

        if (distanceToWalkpoint.magnitude < 1f) {
            isWalkPointSet = false;
            isInvestigating = false;
        }

        //stateText.text = "Investigating";
        //stateText.color = Color.gray;
    }

    private void AttackPlayer() {
        behaviourState = BehaviourState.attacking;

        //stateText.text = "Attacking";
        //stateText.color = Color.red;

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!hasAlreadyAttacked) {
            
            //Attack here
            if (anim == null) Attack();

            hasAlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack() {
        hasAlreadyAttacked = false;
    }

    private void Attack() {
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, transform.rotation);
        //projectile.GetComponent<ParticleCollisionInstance>().SetDamageAmount(damage);
        Destroy(projectile, 20f);
    }

    public void TakeDamage(float _damage) {
        if (behaviourState == BehaviourState.patroling) {
            
            lastPlayerPosition = player.transform.position;
            isInvestigating = true;
        }

        health -= _damage;

        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject, 0.1f);
    }

    private void HandleAnimations() {
        if (behaviourState == BehaviourState.patroling) {
            anim.SetBool("isAttacking", false);
            Debug.Log("Patroling!!");
            anim.SetFloat("Speed", 0.5f);
        }

        if (behaviourState == BehaviourState.chasing) {
            anim.SetBool("isAttacking", false);
            anim.SetFloat("Speed", 1f);
        }

        if (behaviourState == BehaviourState.investigating) {
            anim.SetBool("isAttacking", false);
            anim.SetFloat("Speed", 0.8f);
        }

        if (behaviourState == BehaviourState.attacking) anim.SetBool("isAttacking", true);
    }

    private void AttackWithAnim() {
        Debug.Log("Anim attack");
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, attackPoint.rotation);
        //projectile.GetComponent<ParticleCollisionInstance>().SetDamageAmount(damage);
        Destroy(projectile, 20f);
    }

    //Debug
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * sightRange);
    }

}
