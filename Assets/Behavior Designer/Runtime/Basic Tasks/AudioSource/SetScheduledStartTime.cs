#if !(UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2)
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAudioSource
{
    [TaskCategory("Basic/AudioSource")]
    [TaskDescription("Changes the time at which a sound that has already been scheduled to play will start. Returns Success.")]
    public class SetScheduledStartTime : Action
    {
        [Tooltip("Time in seconds")]
        float time = 0;

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

            audioSource.SetScheduledStartTime(time);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            time = 0;
        }
    }
}
#endif