using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    GameObject[] roamPoints;

    UnityEngine.AI.NavMeshAgent navAgent;

    public float pauseTime = 1.5f;

    enum State { 
        Roaming,
        AttackingPlayer
    }

    State _state;

    State state {
        get { return _state; }
        set {
            EnterState(_state, value);
            _state = value; 
        }
    }

    Coroutine currentStateRoutine;

    void EnterState(State oldState, State newState) {

        if (currentStateRoutine != null)
            StopCoroutine(currentStateRoutine);

        Debug.LogFormat("Changing state from {0} to {1}", oldState.ToString(), newState.ToString());

        switch (newState) {
            case State.Roaming:
                currentStateRoutine = StartCoroutine(RunRoaming());
                break;
            case State.AttackingPlayer:
                currentStateRoutine = StartCoroutine(RunAttacking());
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }

    }

    IEnumerator RunRoaming() {
        do
        {
            navAgent.Resume();
            var newDestination = roamPoints[Random.Range(0, roamPoints.Length)];
            navAgent.SetDestination(newDestination.transform.position);

            while (true)
            {
                if (navAgent.isOnNavMesh == false)
                {
                    Debug.LogError("EnemyAI is not a navmesh");
                    yield break;
                }


                if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance && (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f))
                {
                    break;
                }

                yield return null;
            }

            float timeSincePause = 0.0f;
            while (timeSincePause < pauseTime)
            {
                timeSincePause += Time.deltaTime;
                yield return null;
            }

        } while (state == State.Roaming);
    
    }


    IEnumerator RunAttacking() {
        state = State.Roaming;
        yield break;
    }

    void OnEnable() {
        roamPoints = GameObject.FindGameObjectsWithTag("Roaming Point");
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        state = State.Roaming;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
