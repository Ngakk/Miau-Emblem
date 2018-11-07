using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
	public class Manager_UI : MonoBehaviour {

		[Header("No tocar")]
		public GameObject generalData;
		public GameObject anotherData;
		public GameObject alieData;
		public GameObject enemyData;
		[Space (20)]

		[Header("Character Info")]
		public Text characterName;
		public Text characterHP;
		public Image characterIcon;
		[Header("Character Stats")]
		public Text characterATK;
		public Text characterDEF;
		public Text characterRES;
		public Text characterSPD;
		[Space (20)]

		[Header("BATTLE STATS YOU")]
		public Image aIcon;
		public Text aName;
		public Text aHP;
		public Text aATK;
		[Header("BATTLE STATS ENEMY")]
		public Image eIcon;
		public Text eName;
		public Text eHP;
		public Text eATK;



		void Start()
		{
			Manager_Static.uiManager = this;
		}

		public void getDataCharacter(GameObject _character)
		{
			generalData.SetActive(true);
			anotherData.SetActive(true);
		}

		public void releaseDataCharacter()
		{
			generalData.SetActive(false);
			anotherData.SetActive(false);
		}

		public void getDataCombat(GameObject _alie, GameObject _enemy)
		{
			alieData.SetActive(true);
			enemyData.SetActive(true);
		}

		public void releaseDataCombat()
		{
			alieData.SetActive(false);
			enemyData.SetActive(false);
		}
	}
}
