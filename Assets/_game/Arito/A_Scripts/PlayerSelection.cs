using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour {

    public GameObject grid;
    public Vector2 clickPosition;
    public Vector2 targetPosition;
    public bool selected;
    public GameObject selectedCharacter;
    public float rayDistance = 50.0f;

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
        if (Physics.Raycast (ray, out hit, rayDistance))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log(hit.point);
            Debug.Log(hit.collider.gameObject.name);

            if (!selected && hit.collider.gameObject.CompareTag("Character"))
            {
                Debug.Log("Selected " + hit.collider.gameObject.name);
                selectedCharacter = hit.collider.gameObject;
                selected = true;
            }
            else if (selected && (grid.GetComponent<Grid>().WorldToCell(hit.point).x <= 3 && grid.GetComponent<Grid>().WorldToCell(hit.point).z <= 3))
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

        selectedCharacter.transform.position = targetPos;
        selectedCharacter = null;
        selected = false;
    }
}
