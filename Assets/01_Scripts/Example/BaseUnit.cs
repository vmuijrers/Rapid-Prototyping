using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Example
{
    public abstract class BaseUnit : MonoBehaviour
    {
        public StateMachine stateMachine { get; private set; }
        public Rigidbody rigidbody { get; private set; }
        public NavMeshAgent navMeshAgent { get; private set; }
        public HealthComponent health { get; private set; }

        protected virtual void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            rigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<HealthComponent>();
        }

        protected virtual void Start()
        {
        
        }

        protected virtual void Update()
        {
        }

        protected virtual void FixedUpdate()
        {

        }

    }

}


