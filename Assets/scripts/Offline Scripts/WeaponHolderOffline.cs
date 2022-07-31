﻿using UnityEngine;

[RequireComponent(typeof(Animator))]

public class WeaponHolderOffline : MonoBehaviour {

    [SerializeField]
    private bool isActive = false;

    private Transform rightHandObj = null;
    private Transform leftHandObj = null;
    private Transform rightElbowObj = null;
    private Transform leftElbowObj = null;
    private Transform lookObj = null;


    [SerializeField]
    private WeaponSelectorOffline weaponSelector = null;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
       
    }


    void OnAnimatorIK(int layerIndex) {

        GameObject activeWeapon = weaponSelector.getActiveWeapon();
        GameObject ikPoints = activeWeapon.transform.Find("IKPoints").gameObject;
        if (ikPoints == null)
            Debug.Log("error! IKPOINTS is not initialized for this weapon");
        foreach( Transform child in ikPoints.GetComponentInChildren<Transform>())
        {
          //  Debug.Log(child.gameObject.name);
            if (child.name == "RightHandHandle")
                rightHandObj = child;
            if (child.name == "LeftHandHandle")
                leftHandObj = child;
            if (child.name == "RightElbowHandle")
                rightElbowObj = child;
            if (child.name == "LeftElbowHandle")
                leftElbowObj = child;
            if (child.name == "LookAtHandle")
                leftElbowObj = child;
        }




        // If the IK is active, set the position and rotation directly to the goal.
        // If the IK is not active, set the position and rotation of the hand and head back to the original position.
        if (isActive) {
            // Set the look target position, if one has been assigned.
            if (lookObj != null) {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookObj.position);
            }
            // Set the right hand target position and rotation, if one has been assigned.
            if (rightHandObj != null) {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }
            // Set the left hand target position and rotation, if one has been assigned.
            if (leftHandObj != null) {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }
            // Set the right elbow target position and rotation, if one has been assigned.
            if (rightElbowObj != null) {
                animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowObj.position);
            }
            // Set the left elbow target position and rotation, if one has been assigned.
            if (leftElbowObj != null) {
                animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
                animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowObj.position);
            }
        } else {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0);
            animator.SetLookAtWeight(0);
        }
    }

}
