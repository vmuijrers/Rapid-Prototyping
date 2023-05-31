using System.Linq;
using UnityEngine;

namespace Example
{
    public class AntState_Wander : AntState
    {
        [SerializeField] private float wanderRange = 5;
        [SerializeField] private float senseRadius;
        [SerializeField] LayerMask foodLayer;
        private Vector3 startPosition;

        public override void Setup()
        {
            base.Setup();
            startPosition = transform.position;

        }

        public override void OnEnter()
        {
            FindNewWanderPosition();
        }

        public override void OnUpdate()
        {
            float distanceToTarget = Vector3.Distance(transform.position, owner.TargetPosition);
            if(distanceToTarget > 0.1f)
            {
                owner.animator.SetFloat("MoveSpeed", 1f);
                owner.navMeshAgent.SetDestination(owner.TargetPosition);
            }
            else
            {
                FindNewWanderPosition();
            }

            //Check for transitions
            Collider[] cols = Physics.OverlapSphere(transform.position, senseRadius, foodLayer);
            if(cols.Length != 0)
            {
                owner.TargetFood = cols[0].gameObject;
                stateMachine.SwitchState(typeof(AntState_GoToFood));
            }
        }

        private void FindNewWanderPosition()
        {
            owner.TargetPosition = startPosition + new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
        }
    }

}


