using UnityEngine;
using System.Collections;

public class vax : MonoBehaviour {
	float diff;
	int years;
	[SerializeField]
	GameObject F1;
	[SerializeField]
	GameObject F2;
	[SerializeField]
	GameObject F3;
	[SerializeField]
	GameObject F4;
	[SerializeField]
	GameObject F5;
	[SerializeField]
	GameObject F6;
	[SerializeField]
	GameObject R1;
	[SerializeField]
	GameObject R2;
	[SerializeField]
	GameObject R3;
	[SerializeField]
	GameObject R4;
	[SerializeField]
	GameObject R8;
	[SerializeField]
	GameObject R9;


	public static int count = 0;
	// Use this for initialization
	void Start () {
		years = GameWideScript.Instance.setTime - 2014;
		R1.SetActive(false);
		R2.SetActive(false);
		R3.SetActive(false);
		R4.SetActive(false);
		R8.SetActive(false);
		R9.SetActive(false);
		count = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		count++;
		diff = 400/years;
		if (count < 5000 / diff)
		{
			F5.transform.position = new Vector3(F5.transform.position.x, F5.transform.position.y + 0.007f * diff, F5.transform.position.z);
			F3.transform.position = new Vector3(F3.transform.position.x, F3.transform.position.y + 0.007f * diff, F3.transform.position.z);
		}
		if (3000 / diff < count  && count < 8000 / diff)
		{
			F2.transform.position = new Vector3(F2.transform.position.x, F2.transform.position.y + 0.01f * diff, F2.transform.position.z);
			F6.transform.position = new Vector3(F6.transform.position.x, F6.transform.position.y + 0.007f * diff, F6.transform.position.z);
		}
		if(5000 / diff < count && count < 10000 / diff)
		{
			F1.transform.position = new Vector3(F1.transform.position.x, F1.transform.position.y + 0.007f * diff, F1.transform.position.z);
			F4.transform.position = new Vector3(F4.transform.position.x, F4.transform.position.y + 0.007f * diff, F4.transform.position.z);

		}
		if(count > 6000 / diff)
		{
			R9.SetActive(true);
			R2.SetActive(true);


		}
		if (count > 8000 / diff)
		{
			R8.SetActive(true);
			R1.SetActive(true);            
		}
		if(count > 10000 / diff)
		{
			R3.SetActive(true);
			R4.SetActive(true);
		}
	}
}
