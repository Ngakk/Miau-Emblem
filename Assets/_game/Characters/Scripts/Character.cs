using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
    public class Character : MonoBehaviour {

        public CharacterStats stats;

        public Animator anim;
        public GameEvent charaWalkFinish;

        public float walkSpeed;
        public float walkRotationSpeed;
        public float walkAnimSpeedRatio;

        void Start() { 

        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Vector3[] temp = new Vector3[2];
                temp[0] = Vector3.right;
                temp[1] = Vector3.right * 10;
                Move(temp);
            }
        }

        //Public stuff
        public void Move(Vector3[] path)
        {
            StartCoroutine("Walk", path);
            StartWalkAnim();
        }


        //Private stuff ( ͡° ͜ʖ ͡°)
        private IEnumerator Walk(Vector3[] path)
        {
            bool walking = true;
            int walkStage = 0;
            float rotationT = 0;
            Vector3 lookAtLerp = transform.forward;
            Vector3 kyoForward = transform.forward;
            while (walking)
            {
                Vector3 dir = path[walkStage] - transform.position;
                //Rotation

                if (transform.forward != dir.normalized) {
                    lookAtLerp = Vector3.Lerp(kyoForward, dir.normalized, rotationT);
                    rotationT += Time.deltaTime * walkRotationSpeed;
                    if (rotationT >= 1)
                        rotationT = 1;
                }
                else
                {
                    kyoForward = transform.forward;
                    rotationT = 0;
                }
                transform.LookAt(transform.position + lookAtLerp);
                //Translation
                transform.Translate(dir.normalized * walkSpeed * Time.deltaTime, Space.World);
                if (dir.magnitude <= walkSpeed*Time.deltaTime*2)
                {
                    transform.position = path[walkStage];
                    walkStage++;
                    if (!(walkStage < path.Length))
                        walking = false;
                }
                yield return null;
            }
            EndWalkAnim();
        }

        private void StartWalkAnim()
        {
            anim.SetBool("Walking", true);
            anim.speed = walkSpeed * walkAnimSpeedRatio;
        }

        private void EndWalkAnim()
        {
            charaWalkFinish.Raise();
            anim.SetBool("Walking", false);
        }
    }
}

/* == What can a character do? ==
 * - Move
 * - Attack
 * - CounterAttack
 * - Heal
 * - Die
 * - Take Damage
 * - 
 */