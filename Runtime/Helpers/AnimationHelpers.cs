using UnityEngine;

namespace GameCore.Helpers
{
    public static class AnimationHelpers
    {
        public static void AddAnimationEvent(AnimationClip animationClip, float time, string functionName)
        {
            AnimationEvent animationEvent = new AnimationEvent {
                time = time,
                functionName = functionName
            };

            animationClip.AddEvent(animationEvent);
        }
    }
}