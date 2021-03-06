using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
    [TaskCategory("Basic/Animator")]
    [TaskDescription("Sets the bool parameter on an animator. Returns Success.")]
    public class SetBoolParameter : Action
    {
        [Tooltip("The name of the parameter")]
        public SharedString paramaterName;
        [Tooltip("The value of the bool parameter")]
        public SharedBool boolValue;
        [Tooltip("Should the value be reverted back to its original value after it has been set?")]
        public bool setOnce;

        private int hashID;
        private Animator animator;

        public override void OnAwake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            if (animator == null) {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }

            hashID = UnityEngine.Animator.StringToHash(paramaterName.Value);

            bool prevValue = animator.GetBool(hashID);
            animator.SetBool(hashID, boolValue.Value);
            if (setOnce) {
                StartCoroutine(ResetValue(prevValue));
            }
            return TaskStatus.Success;
        }

        public IEnumerator ResetValue(bool origVale)
        {
            yield return null;
            animator.SetBool(hashID, origVale);
        }

        public override void OnReset()
        {
            if (paramaterName.Value != null) {
                paramaterName.Value = "";
            }
            if (boolValue != null) {
                boolValue.Value = false;
            }
        }
    }
}