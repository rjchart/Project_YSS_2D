using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
    [TaskCategory("Basic/AudioSource")]
    [TaskDescription("Stores the time samples value of the AudioSource. Returns Success.")]
    public class GetTimeSamples : Action
    {
        [Tooltip("The time samples value of the AudioSource")]
        public SharedFloat storeValue;

        private AudioSource audioSource;

        public override void OnAwake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        public override TaskStatus OnUpdate()
        {
            if (audioSource == null) {
                Debug.LogWarning("AudioSource is null");
                return TaskStatus.Failure;
            }

            storeValue.Value = audioSource.timeSamples;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (storeValue != null) {
                storeValue.Value = 1;
            }
        }
    }
}