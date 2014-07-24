using UnityEngine;
using System.Collections;

public class TrnthUiNavigationButtonPop : TrnthUiNavigationButton {
	public void execute(){
		nav.pop();
		//Debug.Log("pop");
	}
	public void OnClick(){
		execute();
	}
}
