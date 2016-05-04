using UnityEngine;
using System.Collections;

public class disco : MonoBehaviour {
	[SerializeField]
	Material f1,f2,f3,f4,f5,f6,f7,f8,f9,f10;
	Color[] farg={Color.red,Color.blue,Color.cyan,Color.green,Color.magenta,Color.yellow};
	int count=0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (count > 50) {
			int rand = Random.Range(0,5);
			f1.color=farg[rand];
			f2.color=farg[rand];
			f3.color=farg[rand];
			f4.color=farg[rand];
			f5.color=farg[rand];
			rand = Random.Range(0,5);
			f6.color=farg[rand];
			f7.color=farg[rand];
			f8.color=farg[rand];
			f9.color=farg[rand];
			f10.color=farg[rand];
			count = 0;
		} else {
			count++;
		}
	}
}
