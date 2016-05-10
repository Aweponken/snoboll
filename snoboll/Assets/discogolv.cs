using UnityEngine;
using System.Collections;

public class discogolv : MonoBehaviour {
	[SerializeField]
	Material f1,f2;
	[SerializeField]
	Light disco;
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
			rand = Random.Range(0,5);
			f2.color=farg[rand];
			rand = Random.Range(0,5);
			disco.color = farg[rand];
			count = 0;
		} else {
			count++;
		}
	}
}
