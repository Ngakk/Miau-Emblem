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
        public GameObject bearer;
        private Vector2 clickPosition;
        private Vector2 targetPosition;
        private bool selected;
        private GameObject selectedCharacter;
        private float rayDistance = 50.0f;
        private GameObject firstChild;
        public int movesLeft;
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
                        selectedCharacter = hit.collider.gameObject;
                        selected = true;
                        currentLayerMask = 1;
                        //movMatrix = matrix.ViewMove(grid.WorldToCell(selectedCharacter.transform.localPosition).x, grid.WorldToCell(selectedCharacter.transform.localPosition).y);
                        Vector3Int selectedPos = selectedCharacter.GetComponent<Character>().coordinates;
                        movMatrix = matrix.ViewMove(selectedPos.x, selectedPos.y);

                        // TESTING MOVMATRIX
                        string matrixDisplay = "";
                        int filas = movMatrix.GetLength(1);
                        int columnas = movMatrix.GetLength(0);
                        for (int y = filas - 1; y >= 0; y--)
                        { 
                            for (int x = 0; x < columnas; x++)
                            {
                                if (movMatrix[x, y] < 10)
                                    matrixDisplay += "0";
                                matrixDisplay += movMatrix[x, y] == (columnas*filas) ? "no" : movMatrix[x, y].ToString();
                                if (x == columnas - 1)
                                    matrixDisplay += "\n";
                                else
                                    matrixDisplay += " | ";
                            }
                        }
                        Debug.Log("movMatrix: \n" + matrixDisplay);

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
                            if (targetChara.CompareTag("Enemy"))
                            {
                                // Check Attack Range
                                int travelDistance = (Mathf.Abs(grid.WorldToCell(selectedCharacter.transform.position).x - grid.WorldToCell(hit.point).x) + Mathf.Abs(grid.WorldToCell(selectedCharacter.transform.position).y - grid.WorldToCell(hit.point).y));
                                //Vector3Int travelPoint = grid.WorldToCell(hit.point);
                                if (travelDistance <= selectedCharacter.GetComponent<Character>().stats.attackRanges[0])
                                {
                                    bearer.GetComponent<Battles>().DukeItOut(selectedCharacter.GetComponent<Character>(), targetChara.GetComponent<Character>());
                                    selectedCharacter.GetComponent<Character>().canMove = false;
                                    ChangeColor(selectedCharacter, new Color(0.2f, 0.2f, 0.2f));
                                }
                                else
                                {
                                    Debug.Log("Out of Range");
                                }
                            }
                            else if (targetChara.CompareTag("Ally") && selectedCharacter.GetComponent<Character>().stats.charClass == CharacterClass.HEALER && targetChara != selectedCharacter)
                            {
                                bearer.GetComponent<Battles>().HealItOut(selectedCharacter.GetComponent<Character>(), targetChara.GetComponent<Character>());
                                selectedCharacter.GetComponent<Character>().canMove = false;
                                ChangeColor(selectedCharacter, new Color(0.2f, 0.2f, 0.2f));
                            }
                        }
                        else
                        {
                            // Check Movement Range && canMove()
                            // int travelDistance = (Mathf.Abs(grid.WorldToCell(selectedCharacter.transform.position).x - grid.WorldToCell(hit.point).x) + Mathf.Abs(grid.WorldToCell(selectedCharacter.transform.position).y - grid.WorldToCell(hit.point).y));
                            Vector3Int travelPoint = grid.WorldToCell(hit.point);
                            if (movMatrix[travelPoint.x, travelPoint.y] <= selectedCharacter.GetComponent<Character>().stats.walkRange && selectedCharacter.GetComponent<Character>().canMove)
                                MoveCharacter(hit.point);
                        }
                        Deselect();
                    }
                    else if (selected)
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
            selectedCharacter.GetComponent<Character>().canMove = false;
            ChangeColor(selectedCharacter, new Color(0.2f, 0.2f, 0.2f));
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

        public void ChangeColor(GameObject _go, Color _color)
        {
            if (_go.CompareTag("Ally"))
            {
                GameObject childboi = null;
                foreach (Transform child in _go.transform)
                {
                    childboi = child.gameObject;
                }
                foreach (Transform child in childboi.transform)
                {
                    if (child.gameObject.CompareTag("ModelCat"))
                    {
                        child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color = _color;
                    }
                }
            }
        }
    }
}
