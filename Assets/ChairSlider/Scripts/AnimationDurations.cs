using UnityEngine;

public class AnimationDurations : MonoBehaviour
{
    [Range(0f, 1f)]
    public float pathAnimationDuration = 0.7f;
    [Range(0f, 1f)]
    public float itemAnimationDuration = 0.3f;

    public static AnimationDurations current;


    private void Awake()
    {
        current = this;
    }

}
