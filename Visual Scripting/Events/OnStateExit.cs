using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

[UnitCategory("State Machines/Events")]
[UnitTitle("On State Exitted")]
public class OnStateExit : ReflectiveEventUnit<OnStateExit>
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