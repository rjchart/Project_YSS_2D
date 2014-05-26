using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
    [TaskCategory("Basic/SharedVariable")]
    [TaskDescription("Sets the SharedBool variable to the specified object. Returns Success.")]
    public class SetSharedBool : Action
    {
        [Tooltip("The value to set the SharedBool to")]
        public SharedBool targetValue;
        [Tooltip("The SharedBool to set")]
        public SharedBool targetVariable;

        public override TaskStatus OnUpdate()
        {
            targetVariable.Value = targetValue.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (targetValue != null) {
                targetValue.Value = false;
            }
            if (targetVariable != null) {
                targetVariable.Value = false;
            }
        }
    }
}