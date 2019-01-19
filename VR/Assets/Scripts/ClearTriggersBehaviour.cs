using UnityEngine;

public class ClearTriggersBehaviour : StateMachineBehaviour
{
    public string[] Triggers;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var trigger in Triggers)
        {
            animator.ResetTrigger(trigger);
        }
    }
}
