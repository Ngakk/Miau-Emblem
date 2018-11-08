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

		void Awake()
		{
			Manager_Static.uiManager = this;
		}

		public void getDataCharacter(GameObject _character)
		{
			generalData.SetActive(true);
			anotherData.SetActive(true);
            Character temp = _character.GetComponent<Character>();
			characterName.text = temp.namae;
			characterHP.text = temp.hp.ToString() + "/" + temp.stats.maxHp.ToString();
			characterIcon.sprite = temp.icon;
			characterATK.text = (temp.stats.atk + temp.weapon.mt).ToString();
			characterDEF.text = temp.stats.def.ToString();
			characterRES.text = temp.stats.res.ToString();
			characterSPD.text = temp.stats.spd.ToString();

		}

		public void releaseDataCharacter()
		{
			generalData.SetActive(false);
			anotherData.SetActive(false);
			characterName.text = "";
			characterHP.text =  "";
			characterIcon.sprite = null;
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
            Character temp = _alie.GetComponent<Character>();
			aIcon.sprite = temp.icon; 
			aName.text = temp.namae;
			aHP.text = temp.hp.ToString();
			aATK.text = (temp.stats.atk + temp.weapon.mt).ToString();
            //Enemy
            temp = _enemy.GetComponent<Character>();
			eIcon.sprite = temp.icon; 
			eName.text = temp.namae;
			eHP.text = temp.hp.ToString();
			eATK.text = (temp.stats.atk + temp.weapon.mt).ToString();
		}

		public void releaseDataCombat()
		{
			alieData.SetActive(false);
			enemyData.SetActive(false);
			//Alie
			aIcon.sprite = null; 
			aName.text = "";
			aHP.text = "";
			aATK.text = "";
			//Enemy
			eIcon.sprite = null;
			eName.text = "";
			eHP.text = "";
			eATK.text = "";
		}
	}
}
