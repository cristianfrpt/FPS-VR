using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoCode : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform playerTarget;
    public Transform playerHead;
    public float shootDistance = 5f;
    public FireOnActive gun;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        setupRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTarget.position);

        float distanceFromPlayer = Vector3.Distance(playerTarget.position, transform.position);

        if(distanceFromPlayer < shootDistance)
        {
            agent.isStopped = true;
            animator.SetBool("Shoot", true);
        }

    }

    public void setupRagdoll()
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = true;
        }
    }

    public void shootEnemy()
    {
        Vector3 playerHeadPos = playerHead.position - Random.Range(0, 0.4f) * Vector3.up;

        gun.spawnPoint.forward = (playerHeadPos - gun.spawnPoint.position).normalized;
        gun.FireBullet();
    }

    public void dead(Vector3 hitPosition)
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = false;
        }
        foreach (var item in Physics.OverlapSphere(hitPosition, 0.0f))
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if(rb)
            {
                rb.AddExplosionForce(1000, hitPosition, 0.3f);
            }
        }

        animator.enabled = false;
        agent.enabled = false;
        this.enabled = false;
    }
}
