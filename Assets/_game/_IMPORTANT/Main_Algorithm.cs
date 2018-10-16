using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_MainAlgorithm", menuName = "Azareth/Stolen", order = 1)]
public class Main_Algorithm : ScriptableObject
{
	public int[,,] matrix;
	public int filas;
	public int columnas;

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
			matrix = new int[filas, columnas, 3];
			Debug.Log("Matrix resized");
		}
	}
}
