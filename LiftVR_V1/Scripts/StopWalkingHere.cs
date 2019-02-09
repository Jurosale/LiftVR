using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWalkingHere : StateMachineBehaviour {

	// This behaviour is meant to stop the patron when they reach the walking point
	// This means the elevator as well as the floor they want to reach

	// 1st iteration: Just getting the business man walking in and maybe out on his floor

	PatronMovement pm;



	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// now I've started walking, but when do i stop 
		// check to see if isWalking has been adjusted
		//if (animator.parameters.

		//if (animator.transform.position == animator.GetComponent<AnimationMovement>().targetWaypoint.transform.position) {

	//	}
//	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}



}
