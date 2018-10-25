using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_MainAlgorithm", menuName = "Azareth/Stolen", order = 1)]
public class Main_Algorithm : ScriptableObject
{
	public int[,,] matrix;
	int[,] movesMatrix;
	public int filas;
	public int columnas;

	public int x_Origen;
	public int y_Origen;

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
					matrix[i,j,0] = 0;
					matrix[i,j,1] = -1;
				}
			}
			Debug.Log("Matrix resized");
		}
	}

	public int[,] makeMap()
	{
		flood_Fill(x_Origen, y_Origen, filas, columnas, 0);
		for(int i = 0; i < filas; i++)
		{
			for(int j = 0; j < columnas; j++)
			{
				movesMatrix[i,j] = matrix[i,j,1];
			}
		}
		return movesMatrix;
	}

	public void ViewMap()
	{
		for(int i = 0; i < filas; i++)
		{
			for(int j = 0; j < columnas; j++)
			{
				Debug.Log("Valor de [" + i + "][" + j +"] es: " + movesMatrix[i,j]);
			}
		}
	}

	public void flood_Fill(int _x, int _y, int _filas, int _columnas, int _round)
	{
		if (_x >= filas || _y >= _columnas)
			return;
		if (_x < 0 || _y < 0)
			return;
		if (matrix[_x,_y,1] > 0)
			return;
		if (matrix[_x,_y,0] != 0)
			return;
		matrix[_x,_y,0] = 1;
		matrix[_x,_y,1] = _round;
		flood_Fill(_x-1, _y, _filas, _columnas, _round + 1);
		flood_Fill(_x, _y-1, _filas, _columnas, _round + 1);
		flood_Fill(_x, _y+1, _filas, _columnas, _round + 1);
		flood_Fill(_x+1, _y, _filas, _columnas, _round + 1);
	}
}
