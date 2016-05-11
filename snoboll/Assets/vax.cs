using UnityEngine;
using System.Collections;

public class vax : MonoBehaviour {

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
	GameObject R5;
	[SerializeField]
	GameObject R6;
	[SerializeField]
	GameObject R7;
	[SerializeField]
	GameObject R8;
	[SerializeField]
	GameObject R9;
	[SerializeField]
	GameObject R10;
	[SerializeField]
	GameObject R11;
	[SerializeField]
	GameObject R12;

	public static int count = 0;
	// Use this for initialization
	void Start () {
		R1.active = false;
		R2.active = false;
		R3.active = false;
		R4.active = false;
		R5.active = false;
		R6.active = false;
		R7.active = false;
		R8.active = false;
		R9.active = false;
		R10.active = false;
		R11.active = false;
		R12.active = false;
	}

	// Update is called once per frame
	void Update () {
		count++;
		if (count < 5000)
		{

			F5.transform.position = new Vector3(F5.transform.position.x, F5.transform.position.y + 0.007f, F5.transform.position.z);
			F3.transform.position = new Vector3(F3.transform.position.x, F3.transform.position.y + 0.007f, F3.transform.position.z);
		}
		if (2000 < count && count < 7000)
		{
			F2.transform.position = new Vector3(F2.transform.position.x, F2.transform.position.y + 0.01f, F2.transform.position.z);
			F6.transform.position = new Vector3(F6.transform.position.x, F6.transform.position.y + 0.007f, F6.transform.position.z);
		}
		if(4000 < count && count < 9000)
		{
			F1.transform.position = new Vector3(F1.transform.position.x, F1.transform.position.y + 0.007f, F1.transform.position.z);
			F4.transform.position = new Vector3(F4.transform.position.x, F4.transform.position.y + 0.007f, F4.transform.position.z);

		}
		if(count > 5000)
		{
			R9.active = true;
			R10.active = true;
			R11.active = true;
			R12.active = true;
			R2.active = true;


		}
		if (count > 7000)
		{
			R8.active = true;
			R1.active = true;            
		}
		if(count > 9000)
		{
			R3.active = true;
			R4.active = true;
			R5.active = true;
			R6.active = true;
			R7.active = true;

		}
	}
}
