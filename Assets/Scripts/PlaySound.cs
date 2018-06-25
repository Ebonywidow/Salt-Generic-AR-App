using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : StateMachineBehaviour {

    private GameObject audioClipPlaying;
    public AudioClip audioClip;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        audioClipPlaying = GameObject.Find("One shot audio");

        if ( !audioClipPlaying )
        {
            AudioSource.PlayClipAtPoint(audioClip, animator.transform.position, 1);
        } else
        {
            Destroy(audioClipPlaying);
            AudioSource.PlayClipAtPoint(audioClip, animator.transform.position, 1);
        }

        //Debug.Log("this is the animation clip length " + stateInfo.length);
        //Debug.Log("this is the loop state of the clip " + stateInfo.loop);
        //Debug.Log("this is the normalizedTime fo the clip " + stateInfo.normalizedTime);

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //Debug.Log("is the gameobject active on the stage " + audioClipPlaying);
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //Debug.Log("The stateinfo loop is " + stateInfo.loop);
        Destroy(audioClipPlaying);    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
