using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mangos
{
	public class Change_Volumen : MonoBehaviour {

		public Text volumen;
		public Slider mainSlider;

		public void Start()
		{
			//Adds a listener to the main slider and invokes a method when the value changes.
			mainSlider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
			mainSlider.value = Manager_Static.GeneralVolumen;
			volumen.text = (mainSlider.value.ToString() + "%");
		}

		// Invoked when the value of the slider changes.
		public void ValueChangeCheck()
		{
			Debug.Log(mainSlider.value);
			Manager_Static.GeneralVolumen = mainSlider.value;
			volumen.text = (mainSlider.value.ToString() + "%");
		}
	}
}
