using UnityEngine;

public class AnimationMessageBehaviour : StateMachineBehaviour
{
    public string StateName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.BroadcastMessage("OnStateEnter", StateName, SendMessageOptions.RequireReceiver);
    }
}