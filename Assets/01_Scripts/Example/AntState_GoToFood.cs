using UnityEngine;

namespace Example
{
    public class AntState_GoToFood : AntState
    {
        [SerializeField] private float eatRange = 1f;

        public override void OnEnter()
        {
            owner.navMeshAgent.SetDestination(owner.TargetFood.transform.position);
        }

        public override void OnUpdate()
        {
            float distanceToFood = Vector3.Distance(transform.position, owner.TargetFood.transform.position);
            if(distanceToFood < eatRange)
            {
                owner.animator.SetTrigger("Attack");
                owner.EatFood(owner.TargetFood);
                stateMachine.SwitchState(typeof(AntState_Wander));
            }
        }
    }

}


