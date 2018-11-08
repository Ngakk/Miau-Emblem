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
			//characterName.text = _character.GetComponent<Character>().namae;
			characterHP.text =  _character.GetComponent<Character>().stats.hp.ToString() + "/" + _character.GetComponent<Character>().stats.maxHp.ToString();
			//characterIcon.sprite = _character.GetComponent<Character>().icon;
			characterATK.text = _character.GetComponent<Character>().stats.atk.ToString();
			characterDEF.text = _character.GetComponent<Character>().stats.def.ToString();
			characterRES.text = _character.GetComponent<Character>().stats.res.ToString();
			characterSPD.text = _character.GetComponent<Character>().stats.spd.ToString();

		}

		public void releaseDataCharacter()
		{
			generalData.SetActive(false);
			anotherData.SetActive(false);
			//characterName.text = "";
			characterHP.text =  "";
			//characterIcon.sprite = "";
			characterATK.text = "";
			characterDEF.text = "";
			characterRES.text = "";
			characterSPD.text = "";
		}

		public void getDataCombat(GameObject _alie, GameObject _enemy)
		{
			alieData.SetActive(true);
			enemyData.SetActive(true);
			//Alie
			//aIcon.sprite = _alie.GetComponent<Character>().icon; 
			//aName.text = _alie.GetComponent<Character>().namae;
			aHP.text = _alie.GetComponent<Character>().stats.hp.ToString();
			aATK.text = _alie.GetComponent<Character>().stats.atk.ToString();
			//Enemy
			//eIcon.sprite = _enemy.GetComponent<Character>().icon; 
			//eName.text = _enemy.GetComponent<Character>().namae;
			eHP.text = _enemy.GetComponent<Character>().stats.hp.ToString();
			eATK.text = _enemy.GetComponent<Character>().stats.atk.ToString();
		}

		public void releaseDataCombat()
		{
			alieData.SetActive(false);
			enemyData.SetActive(false);
			//Alie
			//aIcon.sprite = ""; 
			//aName.text = "";
			aHP.text = "";
			aATK.text = "";
			//Enemy
			//eIcon.sprite = "";
			//eName.text = "";
			eHP.text = "";
			eATK.text = "";
		}
	}
}
