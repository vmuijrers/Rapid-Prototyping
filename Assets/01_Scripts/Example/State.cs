using UnityEngine;

namespace Example
{
    public abstract class State : MonoBehaviour
    {
        protected StateMachine stateMachine;
        public void SetStateMachine(StateMachine machine) { stateMachine = machine; }

        public virtual void Setup() { }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
    }

}


