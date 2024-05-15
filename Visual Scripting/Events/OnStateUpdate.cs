using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

[UnitCategory("State Machines/Events")]
[UnitTitle("On State Update")]
public class OnStateUpdate : ReflectiveEventUnit<OnStateUpdate>
{
    [OutputType(typeof(string))]
    public ValueOutput CurrentState;

    public static void Invoke(GameObject stateMachineObject, string currentState)
    {
        ModularInvoke(stateMachineObject, ("CurrentState", currentState));
    }
}