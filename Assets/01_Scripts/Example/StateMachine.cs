using System.Collections.Generic;
using UnityEngine;

namespace Example
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<System.Type, State> states = new Dictionary<System.Type, State>();
        public State startState;
        public State currentState;

        public void Start() {
            State[] allStates = GetComponents<State>();   
            foreach(State s in allStates)
            {
                s.SetStateMachine(this);
                s.Setup();
                states.Add(s.GetType(), s);
            }
            SwitchState(startState.GetType());
        }

        public void Update()
        {
            currentState?.OnUpdate();
        }

        public void FixedUpdate()
        {
            currentState?.OnFixedUpdate();
        }

        public void SwitchState(System.Type stateType) {

            if (states.ContainsKey(stateType))
            {
                currentState?.OnExit();
                currentState = states[stateType];
                currentState?.OnEnter();
            }
            else
            {
                Debug.LogWarning("State not found in dictionary!");
            }
        }

    }

}


