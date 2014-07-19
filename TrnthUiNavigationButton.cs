using UnityEngine;
using System.Collections;

public class TrnthUiNavigationButton : MonoBehaviour {
	public TrnthUiNavigation nav;
	void Awake(){
		if(!nav)nav=TrnthUiNavigation.main;
	}
}
