using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mangos
{
    public enum MFM : int ///aka Matrix floor meaning
    {
        MOVEMENT_MAP,
        OCCUPIED_BY,
        OBJECT_DATA
    }

    [CreateAssetMenu(fileName = "_MainAlgorithm", menuName = "Azareth/Stolen", order = 1)]
    public class Main_Algorithm : ScriptableObject
    {
        public int[,,] matrix;
        int[,] movesMatrix;
        GameObject[,] characters;
        public int filas;
        public int columnas;

        public int x_Origen;
        public int y_Origen;

        public void ResizeMatrix()
        {
            if (filas < 0 || columnas < 0)
            {
                Debug.Log("No uses valores negativos");
                return;
            }
            else if (filas == 0 || columnas == 0)
            {
                Debug.Log("No poner valores de 0");
            }
            else if (filas != 0 && columnas != 0)
            {
                matrix = new int[filas, columnas, 4];
                movesMatrix = new int[filas, columnas];
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        matrix[i, j, 0] = 0;
                        matrix[i, j, 1] = filas * columnas;
                    }
                }
                Debug.Log("Matrix resized");
            }
        }

        public int[,] MakeMap()
        {
            FloodFill(x_Origen, y_Origen, filas, columnas, 0);
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    movesMatrix[i, j] = matrix[i, j, 1];
                }
            }
            return movesMatrix;
        }

        public void ViewMap()
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Debug.Log("Valor de [" + i + "][" + j + "] es: " + movesMatrix[i, j]);
                }
            }
        }

        public void FloodFill(int _x, int _y, int _filas, int _columnas, int _round)
        {
            if (_x >= filas || _y >= _columnas)
                return;
            if (_x < 0 || _y < 0)
                return;
            if (_round > _filas + _columnas)
                return;
            if (matrix[_x, _y, 1] > _round)
            {
                matrix[_x, _y, 1] = _round;
            }
            FloodFill(_x + 1, _y, _filas, _columnas, _round + 1);
            FloodFill(_x, _y + 1, _filas, _columnas, _round + 1);
            FloodFill(_x - 1, _y, _filas, _columnas, _round + 1);
            FloodFill(_x, _y - 1, _filas, _columnas, _round + 1);
        }

        public GameObject GetCharacterDataAt(int x, int y)
        {
            return characters[x, y];
        }

        public void OnCharacterMoved(int xFrom, int yFrom, int xTo, int yTo)
        {
            characters[xTo, yTo] = characters[xFrom, yFrom];
            characters[xFrom, yFrom] = null;
        }
      	public void resizeMatrix()
      	{
      		if(filas < 0 || columnas < 0)
      		{
      			Debug.Log("No uses valores negativos");
      			return;
      		}
      		else if(filas == 0 || columnas == 0)
      		{
      			Debug.Log("No poner valores de 0");
      		}
      		else if(filas != 0 && columnas != 0)
      		{
      			matrix = new int[filas, columnas, 4];
      			movesMatrix = new int[filas,columnas];
      			for(int i = 0; i < filas; i++)
      			{
      				for(int j = 0; j < columnas; j++)
      				{
      					matrix[i,j,0] = filas * columnas;
      				}
      			}
      			Debug.Log("Matrix resized");
      			for(int j = 0; j < 3; j++)
      			{
      				matrix[Mathf.RoundToInt(Random.Range(0, filas)),Mathf.RoundToInt(Random.Range(0, columnas)),1] = -1;
      			}
      		}
      	}

      	public int[,] makeMap()
      	{
      		flood_Fill(x_Origen, y_Origen, filas, columnas, 0);
      		Debug.Log("Matrix Filled ( ͡ ͡° ͜ ʖ ͡ ͡°)...");
      		for(int i = 0; i < filas; i++)
      		{
      			for(int j = 0; j < columnas; j++)
      			{
      				movesMatrix[i,j] = matrix[i,j,0];
      			}
      		}
      		return movesMatrix;
      	}

        public void InsertCharacterAt(GameObject chara, int x, int y)
        {
            if(x < columnas && y < filas && characters[x, y] == null)
            {
                characters[x, y] = chara;
            }
        }

        public void RemoveCharacterAt(int x, int y)
        {
            characters[x, y] = null;
        }

        public void flood_Fill(int _x, int _y, int _filas, int _columnas, int _round)
      	{
      		if (_x >= filas || _y >= _columnas)
      			return;
      		if (_x < 0 || _y < 0)
      			return;
      		if(_round > _filas + _columnas)
      			return;
      		if(matrix[_x,_y,1] < 0)
      			return;
      		if(matrix[_x,_y,0] > _round)
      		{
      			matrix[_x,_y,0] = _round;
      		}
      		flood_Fill(_x + 1, _y, _filas, _columnas, _round + 1);
      		flood_Fill(_x, _y + 1, _filas, _columnas, _round + 1);
      		flood_Fill(_x - 1, _y, _filas, _columnas, _round + 1);
      		flood_Fill(_x, _y - 1, _filas, _columnas, _round + 1);
      	}
    }
}
