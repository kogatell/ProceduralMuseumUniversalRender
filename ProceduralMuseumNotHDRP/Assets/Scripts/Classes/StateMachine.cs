using System.Collections;
using UnityEngine;
namespace Assets.Scripts
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State state;

        public void SetState(State _state)
        {
            state = _state;
        }
    }
}
