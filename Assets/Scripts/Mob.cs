using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
    // Configuration
    public Transform[] patrolTargets;   // Les points de patrouille
    public float detectionRadius = 2f;  // Rayon de d�tection du joueur
    public float chaseSpeed = 1.7f;     // Vitesse pendant la chasse
    public float patrolSpeed = 0.9f;    // Vitesse pendant la patrouille
    public float soundCooldown = 10f;   // Temps minimum entre deux r�p�titions de sons
    public LayerMask layerMask;

    // Composants
    private NavMeshAgent agent;         // Composant de navigation
    private Animator anim;              // Composant Animator pour les animations
    private MovementBehaviour player;              // R�f�rence au joueur

    // �tat du mob
    public bool isChasing = false;     // Si le mob poursuit le joueur
    private Vector3 currentTarget;      // Cible actuelle de la patrouille
    private float lastSoundTime;        // Derni�re fois o� le son a �t� jou�

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<MovementBehaviour>();

        StartCoroutine(PatrolRoutine());
    }

    private void Update()
    {
        DetectPlayer();
        if(Vector3.Distance(transform.position, currentTarget) < 1f && !isChasing)
        {
            NewDestination();
        }
    }

    // Routine de patrouille
    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!isChasing) // Si le mob ne poursuit pas, il patrouille
            {
                NewDestination();
                yield return new WaitForSeconds(30f); // Temps entre chaque changement de cible
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void NewDestination()
    {
        int randomTargetIndex = Random.Range(0, patrolTargets.Length);


        currentTarget = patrolTargets[randomTargetIndex].position;
        agent.speed = patrolSpeed;
        agent.SetDestination(currentTarget);
    }

    // D�tection du joueur
    private void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < detectionRadius)
        {
            ChasePlayer();
        }
        else if (isChasing)
        {

            StopChasingAndReturnToPatrol();
        }
    }

    // Commencer la poursuite du joueur
    private void ChasePlayer()
    {
        Debug.Log("Chase");
        if (!isChasing)
        {
            isChasing = true;
            agent.speed = chaseSpeed;

            // Jouer les sons si le cooldown est termin�
            if (Time.time - lastSoundTime > soundCooldown)
            {
                SoundManager.instance.Repere();
                lastSoundTime = Time.time;
            }
        }

        // Mettre � jour la destination du mob vers le joueur
        agent.SetDestination(player.transform.position);
    }

    // Arr�ter la poursuite et retourner � la patrouille
    private void StopChasingAndReturnToPatrol()
    {
        isChasing = false;

        // Choisir un nouveau point de patrouille
        int randomTargetIndex = Random.Range(0, patrolTargets.Length);
        currentTarget = patrolTargets[randomTargetIndex].position;
        agent.SetDestination(currentTarget);
        agent.speed = patrolSpeed;
    }

    // Interaction avec les portes
    private void OnTriggerEnter(Collider other)
    {
        // Interaction avec le joueur (d�clenche le screamer)
        if (other.CompareTag("Player"))
        {
            Screamer();
        }
    }

    // Lancer l'animation et le son du screamer
    public void Screamer()
    {
        player.gameObject.SetActive(false);
        anim.SetTrigger("Screamer");
        agent.SetDestination(transform.position); // Arr�ter le mouvement pendant le screamer
        StartCoroutine(GameManager.instance.Screamer());
        SoundManager.instance.ScreamerSfx();
    }

    // Visualiser la sph�re de d�tection dans l'�diteur
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
