using UnityEngine;
using System;
using System.Collections;
[ExecuteInEditMode]
public class TrnthUiNavigationButtonPush : TrnthUiNavigationButton {
	public GameObject target;
	public Action<GameObject> onAfterSapwn;
	public bool onClick=true;
	public void execute(){
		if(!target)return;
		nav.push(target,this);
	}
	void OnClick(){
		if(onClick)execute();
	}
	void OnDrawGizmos(){
		if (target != null) {
            Gizmos.color = Color.blue;
            var pos=target.transform.position;
            // var widget=target.GetComponent<UIWidget>();
            // if(widget)pos=widget.drawRegion;
            Gizmos.DrawLine(transform.position, pos);
        }
	}
}
