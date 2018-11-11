using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class PlayerSelection : MonoBehaviour {

        public Grid grid;
        public Main_Algorithm matrix;
        public LayerMask[] masks;
        public int maxMoves = 3;
        private Vector2 clickPosition;
        private Vector2 targetPosition;
        private bool selected;
        private GameObject selectedCharacter;
        private float rayDistance = 50.0f;
        private GameObject firstChild;
        private int movesLeft;
        private int currentLayerMask = 0;
        private int[,] movMatrix;

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
                    if (!selected && hit.collider.gameObject.CompareTag("Ally"))
                    {
                        Debug.Log("Selected " + hit.collider.gameObject.name);
                        selectedCharacter = hit.collider.gameObject;
                        selected = true;
                        currentLayerMask = 1;
                        Debug.Log("Created Movement Matrix with center at " + grid.WorldToCell(selectedCharacter.transform.position).x + ", " + grid.WorldToCell(selectedCharacter.transform.position).y);
                        movMatrix = matrix.ViewMove(grid.WorldToCell(selectedCharacter.transform.localPosition).x, grid.WorldToCell(selectedCharacter.transform.localPosition).y);
                        Manager_Static.uiManager.getDataCharacter(selectedCharacter.gameObject);

                        foreach (Transform child in selectedCharacter.transform)
                        {
                            firstChild = child.gameObject;
                        }
                        foreach (Transform child in firstChild.transform)
                        {
                            if (child.CompareTag("Model"))
                            {
                                child.gameObject.GetComponent<SkinnedMeshRenderer>().material = Manager_Static.materialsManager.GetMaterial(CharacterMats.SELECTED);
                            }
                        }
                    }
                    else if (selected && hit.collider.gameObject.CompareTag("Map"))
                    {
                        GameObject targetChara = matrix.GetCharacterDataAt(grid.WorldToCell(hit.point).x, grid.WorldToCell(hit.point).y);
                        if (targetChara)
                        {
                            Debug.Log("found " + targetChara);
                            if (targetChara.CompareTag("Enemy"))
                            {
                                Debug.Log("Value at " + grid.WorldToCell(hit.point).x + ", " + grid.WorldToCell(hit.point).y + " = " + movMatrix[grid.WorldToCell(hit.point).x, grid.WorldToCell(hit.point).y]);
                                if (movMatrix[grid.WorldToCell(hit.point).x, grid.WorldToCell(hit.point).y] <= selectedCharacter.GetComponent<Character>().stats.attackRanges[0])
                                {
                                    Debug.Log("Fight with " + targetChara);
                                }
                                else
                                {
                                    Debug.Log("Out of Range");
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Value at " + grid.WorldToCell(hit.point).x + ", " + grid.WorldToCell(hit.point).y + " = " + movMatrix[grid.WorldToCell(hit.point).x, grid.WorldToCell(hit.point).y]);
                            if (movMatrix[grid.WorldToCell(hit.point).x, grid.WorldToCell(hit.point).y] <= selectedCharacter.GetComponent<Character>().stats.walkRange)
                                MoveCharacter(hit.point);
                        }
                        Deselect();
                    }
                    else
                    {
                        Deselect();
                    }
                }
            }
            Manager_Static.uiManager.releaseDataCharacter();
        }

        public void MoveCharacter(Vector3 targetPos)
        {
            /// Movement
            Vector3Int targetPosGrid = grid.WorldToCell(targetPos);
            Vector3 finalPos = grid.GetCellCenterLocal(targetPosGrid);
            Vector3[] movementArray = { finalPos };
            selectedCharacter.GetComponent<Character>().Move(movementArray);
            movesLeft--;
            if (movesLeft <= 0)
            {
                Manager_Static.turnsManager.ToggleTurn();
            }
        }

        private void Deselect()
        {
            foreach (Transform child in firstChild.transform)
            {
                if (child.CompareTag("Model"))
                {
                    child.gameObject.GetComponent<SkinnedMeshRenderer>().material = Manager_Static.materialsManager.GetMaterial(CharacterMats.DEFAULT);
                }
            }
            selectedCharacter = null;
            selected = false;
            currentLayerMask = 0;
        }
    }
}