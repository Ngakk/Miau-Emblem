using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour {

    public GameObject grid;
    public Vector2 clickPosition;
    public Vector2 targetPosition;
    public bool selected;
    public GameObject selectedCharacter;

    public void OnCellClick(float x, float y)
    {
        grid.GetComponent<Grid>().WorldToCell(new Vector3(x, y, 0));
        /* if (getTerrain(x, y)) {
         *     selectedCharacter = getCharacterMatrix(x, y);
         *     selected = true;
         * }
         */
    }

    public void MoveCharacter(float x, float y)
    {
        if (selected)
        {
            /*
            targetPosition = new Vector2(x, y);
            if (getMovementMatrix()[][] <= selectedCharacter.getComponent<CharacterScript>().GetMovementRange()){
                CallMoveCharacter(targetPosition);
            }
            */
        }
    }
}
