using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class TrnthUiNavigationButtonPush : TrnthUiNavigationButton {
	public GameObject target;
	public void execute(){
		nav.push(target);
	}
	void OnClick(){
		execute();
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
