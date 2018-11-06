using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class PlayerSelection : MonoBehaviour {

        public Grid grid;
        public Vector2 clickPosition;
        public Vector2 targetPosition;
        public bool selected;
        public GameObject selectedCharacter;
        public float rayDistance = 50.0f;
        public LayerMask[] masks;
        public GameObject firstChild;

        public int currentLayerMask = 0;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnCellClick(Input.mousePosition);
            }
        }

        public void OnCellClick(Vector3 mousePos)
        {
            /// Grid Based

            //grid.GetComponent<Grid>().WorldToCell(new Vector3(x, y, 0));
            /* if (getTerrain(x, y)) {
             *     selectedCharacter = getCharacterMatrix(x, y);
             *     selected = true;
             * }
             */

            // TODO
            // Check Raycast to Floor (layermask)
            // move character with Characterscript.move(vector3[])

            /// Raycast

            // if (selectedCharacter.getComponent<stats>().canMove)
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, masks[currentLayerMask]))
            {
                Debug.Log(hit.collider.gameObject.name);

                if (!selected && hit.collider.gameObject.CompareTag("Ally"))
                {
                    Debug.Log("Selected " + hit.collider.gameObject.name);
                    selectedCharacter = hit.collider.gameObject;
                    selected = true;
                    currentLayerMask = 1;
                    
                    foreach (Transform child in selectedCharacter.transform)
                    {
                        firstChild = child.gameObject;
                    }
                    foreach (Transform child in firstChild.transform)
                    {
                        if (child.CompareTag("Model"))
                        {
                            child.gameObject.GetComponent<SkinnedMeshRenderer>().material = Manager_Static.materialsManager.GetMaterial(CharacterMats.PLAYER);
                        }
                    }
                }
                else if (selected && hit.collider.gameObject.CompareTag("Map"))
                {
                    Debug.Log("Movement");
                    MoveCharacter(hit.point);
                    Deselect();
                }
                else if (selected && hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Fight with " + hit.collider.gameObject.name);
                    Deselect();
                }
            }
        }

        public void MoveCharacter(Vector3 targetPos)
        {
            /// Movement

            foreach (Transform child in firstChild.transform)
            {
                if (child.CompareTag("Model"))
                {
                    child.gameObject.GetComponent<SkinnedMeshRenderer>().material = Manager_Static.materialsManager.GetMaterial(CharacterMats.DEFAULT);
                }
            }
            Vector3Int targetPosGrid = grid.WorldToCell(targetPos);
            Vector3 centerCell = grid.GetCellCenterLocal(targetPosGrid);
            Vector3 finalPos = new Vector3(centerCell.x, centerCell.y, centerCell.z);
            Vector3[] movementArray = { finalPos };
            selectedCharacter.GetComponent<Character>().Move(movementArray);
        }

        private void Deselect()
        {
            selectedCharacter = null;
            selected = false;
            currentLayerMask = 0;
        }
    }
}