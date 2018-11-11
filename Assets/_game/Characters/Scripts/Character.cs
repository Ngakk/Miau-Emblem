using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {
    public class Character : MonoBehaviour {
        [Header("Datos de personaje")]
        public string namae;
        [Tooltip("Equipo al que pertenece")]
        public Team team;
        public Sprite icon;
        public CharacterStats stats;
        public WeaponStats weapon;
        [Header("Settings de caminado")]
        public float walkSpeed;
        public float walkRotationSpeed;
        public float walkAnimSpeedRatio;
        [Header("Referencias necesarias")]
        public Grid grid;
        public Main_Algorithm masterMatrix;
        [Header("Game Events")]
        public GameEvent charaWalkFinish;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Fighter fight;
        [HideInInspector]
        public int hp;
        [HideInInspector]
        public Vector3Int coordinates;
        [HideInInspector]
        public bool canMove;

        void Start() {
            LocateInGrid();
            canMove = true;
            hp = stats.maxHp;
            fight = GetComponentInChildren<Fighter>();
            anim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
            if (fight)
                fight.controller = this;
        }

        private void LocateInGrid()
        {
            coordinates = grid.WorldToCell(transform.position);
            Debug.Log(namae + " is at " +coordinates);
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
            Debug.Log("Character.Move() start");
            StartWalkAnim();
            StartCoroutine("Walk", path);
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
            anim.speed = walkSpeed * walkAnimSpeedRatio;
            anim.SetBool("IsWalking", true);
            
        }

        private void EndWalkAnim()
        {
            charaWalkFinish.Raise();
            anim.SetBool("IsWalking", false);
        }

        public void OnDead()
        {
            GetComponent<BoxCollider>().enabled = false;
            masterMatrix.RemoveCharacterAt(coordinates.x, coordinates.y);
            Manager_Static.playerSelectionManager.maxMoves--;
            Invoke("PlayDeadAnimation", Manager_Static.battles.delay);
        }

        private void PlayDeadAnimation()
        {
            anim.SetTrigger("Die");
        }

        public void OnDeadAnimEnd()
        {
            Destroy(gameObject);
        }

        public void OnWin()
        {
            anim.SetTrigger("Win");
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
