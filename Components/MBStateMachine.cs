using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UDT.StateMachines.ForGameObjects;
using UnityEngine;

public class MBStateMachine : MonoBehaviour
{
    [SerializeField]
    private SerializableDictionary<string, List<MBStateHandle>> States = new();
    [ReadOnly]
    [SerializeField]
    private string _currentState = "";
    private string _previousState = "";

    private void OnValidate()
    {
        foreach(var state in States.Keys)
        {
            foreach(var handle in States[state])
            {
                handle.fsm = this;
            }
        }

        var scriptStates = gameObject.GetAllStateNames();
        foreach(var state in scriptStates)
            ForceState(state);
    }

    private void Update()
    {
        OnStateUpdate.Invoke(gameObject, _currentState);
    }

    public void SetState(string stateName)
    {
        if(States.ContainsKey(stateName))
        {
            foreach (var currentState in States.Keys)
            {
                foreach (var currentHandle in States[currentState])
                {
                    currentHandle.CachedComponent.enabled = stateName == currentState;
                }
            }
            OnStateExit.Invoke(gameObject, _currentState, stateName);
            _currentState = stateName;
            gameObject.SetState(stateName);
            OnStateEnter.Invoke(gameObject, _previousState, _currentState);
        }
    }

    /// <summary>
    /// Forces a State to be present in the States Dictionary
    /// </summary>
    /// <param name="stateName"></param>
    public void ForceState(string stateName)
    {
        if (!States.ContainsKey(stateName))
            States.Add(stateName, new());
    }

    [Serializable]
    public class MBStateHandle
    {
        [HideInInspector]
        public MBStateMachine fsm;
        [Dropdown("GetMonobehaviours")]
        public string MonoBehaviour;

        private MonoBehaviour _cachedComponent;
        public MonoBehaviour CachedComponent => _cachedComponent ??= (MonoBehaviour)fsm.GetComponent(Type.GetType(MonoBehaviour));
        
        public DropdownList<string> GetMonobehaviours()
        {
            DropdownList<string> list = new();
            list.Add("None", "None");

            foreach (var MB in fsm.gameObject.GetComponents<MonoBehaviour>())
            {
                list.Add(MB.name, MB.GetType().AssemblyQualifiedName);
            }

            return list;
        }

    }
}
