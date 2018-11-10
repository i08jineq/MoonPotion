using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame
{
    public class CharacterPawn : MonoBehaviour
    {
        public Animator animator;
        public float rotateSpeed = 10;
        public float movementSpeed = 10;
        public float squareCutOffRadius = 0.9f;

        private Vector3 targePosition = Vector3.zero;

        private bool isMovingToPoint = false;
        private bool isRotating = false;

        public void MoveToPosition(Vector3 _targetPosition)
        {
            targePosition = _targetPosition;
            StartCoroutine(MoveToTargetEnumerator());
        }

        private IEnumerator MoveToTargetEnumerator()
        {
            if(isMovingToPoint)
            {
                yield break;
            }
            isMovingToPoint = true;
            yield return RotateToTargetEnumerator(targePosition);

            //move
            float distance = (targePosition - transform.position).sqrMagnitude;

            while(distance > squareCutOffRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, targePosition, movementSpeed * Time.deltaTime);
                yield return null;
            }
            isMovingToPoint = false;
        }

        public void RotateToLocation(Vector3 target)
        {
            StartCoroutine(RotateToTargetEnumerator(target));
        }

        public IEnumerator RotateToTargetEnumerator(Vector3 target)
        {
            if(isRotating)
            {
                yield break;
            }
            isRotating = true;
            Vector3 targetDirection = target - transform.position;
            //rotate
            while (targetDirection != transform.forward)
            {
                transform.forward = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * rotateSpeed, 1);
                yield return null;
            }
            isRotating = false;
        }

        public void PlayAnimation(string animationName)
        {
            animator.Play(animationName);
        }
    }
}