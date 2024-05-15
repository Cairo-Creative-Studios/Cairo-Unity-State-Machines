using Unity.VisualScripting;
using UnityEngine;

[UnitCategory("State Machines/Events")]
[UnitTitle("On State Entered")]
public class OnStateEnter : ReflectiveEventUnit<OnStateEnter>
{
    [OutputType(typeof(string))]
    public ValueOutput CurrentState;

    [OutputType(typeof(string))]
    public ValueOutput NextState;

    public static void Invoke(GameObject stateMachineObject, string currentState, string nextStateName)
    {
        ModularInvoke(stateMachineObject, ("CurrentState", currentState), ("NextState", nextStateName));
    }
}