using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class PlayerSelection : MonoBehaviour {

        public Grid grid;
        private Vector2 clickPosition;
        private Vector2 targetPosition;
        private bool selected;
        private GameObject selectedCharacter;
        private float rayDistance = 50.0f;
        public LayerMask[] masks;
        private GameObject firstChild;
        public int maxMoves = 3;
        public int movesLeft;

        public int currentLayerMask = 0;

        private void Awake()
        {
            Manager_Static.playerSelectionManager = this;
            movesLeft = maxMoves;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnCellClick(Input.mousePosition);
            }
        }

        public void OnCellClick(Vector3 mousePos)
        {
            if (Manager_Static.turnsManager.currentGameState == GameState.PLAYER_TURN)
            {
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
                                int matLength = child.GetComponent<SkinnedMeshRenderer>().materials.Length - 1;
                                Debug.Log("mat: " + Manager_Static.materialsManager.GetMaterial(CharacterMats.PLAYER));
                                child.gameObject.GetComponent<SkinnedMeshRenderer>().materials[matLength] = Manager_Static.materialsManager.GetMaterial(CharacterMats.PLAYER);
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
        }

        public void MoveCharacter(Vector3 targetPos)
        {
            /// Movement

            foreach (Transform child in firstChild.transform)
            {
                if (child.CompareTag("Model"))
                {
                    int matLength = child.GetComponent<SkinnedMeshRenderer>().materials.Length - 1;
                    Debug.Log("mat: " + child.GetComponent<SkinnedMeshRenderer>().materials[matLength]);
                    child.gameObject.GetComponent<SkinnedMeshRenderer>().materials[matLength] = Manager_Static.materialsManager.GetMaterial(CharacterMats.DEFAULT);
                }
            }
            Vector3Int targetPosGrid = grid.WorldToCell(targetPos);
            Vector3 centerCell = grid.GetCellCenterLocal(targetPosGrid);
            Vector3 finalPos = new Vector3(centerCell.x, centerCell.y, centerCell.z);
            Vector3[] movementArray = { finalPos };
            selectedCharacter.GetComponent<Character>().Move(movementArray);
            Deselect();
            movesLeft--;
            if (movesLeft <= 0)
            {
                Manager_Static.turnsManager.ToggleTurn();
            }
        }

        private void Deselect()
        {
            selectedCharacter = null;
            selected = false;
            currentLayerMask = 0;
        }
    }
}