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
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log(hit.point);
                Debug.Log(hit.collider.gameObject.name);

                if (!selected && hit.collider.gameObject.CompareTag("Character"))
                {
                    Debug.Log("Selected " + hit.collider.gameObject.name);
                    selectedCharacter = hit.collider.gameObject;
                    selected = true;
                    currentLayerMask = 1;
                    foreach (Transform child in selectedCharacter.transform)
                    {
                        if (child.CompareTag("Model"))
                        {
                            child.gameObject.GetComponent<SkinnedMeshRenderer>().material = Manager_Static.materialsManager.GetMaterial(CharacterMats.PLAYER);
                        }
                    }
                }
                else if (selected && (grid.WorldToCell(hit.point).x <= 3 && grid.WorldToCell(hit.point).z <= 3))
                {
                    MoveCharacter(hit.point);
                }
            }
        }

        public void MoveCharacter(Vector3 targetPos)
        {
            /*
            targetPosition = new Vector2(x, y);
            if (getMovementMatrix()[][] <= selectedCharacter.getComponent<CharacterScript>().GetMovementRange()){
                CallMoveCharacter(targetPosition);
            }
            */

            /// Movement

            foreach (Transform child in selectedCharacter.transform)
            {
                if (child.CompareTag("Model"))
                {
                    child.gameObject.GetComponent<SkinnedMeshRenderer>().material = Manager_Static.materialsManager.GetMaterial(CharacterMats.DEFAULT);
                }
            }
            Vector3Int targetPosGrid = grid.WorldToCell(targetPos);
            Vector3 centerCell = grid.GetCellCenterLocal(targetPosGrid);
            Vector3 finalPos = new Vector3(centerCell.x + grid.cellSize.x / 2, centerCell.y, centerCell.z + grid.cellSize.y / 2);
            selectedCharacter.transform.parent.transform.position = finalPos;
            selectedCharacter = null;
            selected = false;
            currentLayerMask = 0;
            
        }
    }
}