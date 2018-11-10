using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
    public class Character : MonoBehaviour {
        public string namae;
        [Tooltip("Equipo al que pertenece")]
        public Team team;
        public Sprite icon;

        public Grid grid;
        public Main_Algorithm masterMatrix;

        public CharacterStats stats;
        public WeaponStats weapon;

        public Animator anim;
        public GameEvent charaWalkFinish;

        public Fighter fight;

        public float walkSpeed;
        public float walkRotationSpeed;
        public float walkAnimSpeedRatio;
        public bool canMove;
        public Vector3Int coordinates;

        public int hp;

        void Start() {
            LocateInGrid();
            canMove = true;
            hp = stats.maxHp;
            fight = GetComponentInChildren<Fighter>();
            if (fight)
                fight.controller = this;
        }

        private void LocateInGrid()
        {
            coordinates = grid.WorldToCell(transform.position);
            Debug.Log(coordinates);
            masterMatrix.InsertCharacterAt(gameObject, coordinates.x, coordinates.y);
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

        public void OnDead()
        {
            masterMatrix.RemoveCharacterAt(coordinates.x, coordinates.y);
            Destroy(gameObject);
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
