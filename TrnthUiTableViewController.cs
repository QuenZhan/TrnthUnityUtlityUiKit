using UnityEngine;
using System.Collections;

public abstract class TrnthUiTableViewController : TrnthPoolBase {
	public UITable table;
	public abstract int length{get;}
	[ContextMenu ("refresh")]
	public void tableRefresh(){
		foreach(Transform e in table.transform){
			// Debug.Log(e.name);
			Despawn(e);
		}
		for(int i=0;i<length;i++){
			var cell=tableCell(i);
			cell.parent=table.transform;
		}
		table.repositionNow=true;;
	}
	public abstract Transform tableCell(int index);
	public override void Awake	() {
		base.Awake();
		if(!prefabCell){
			prefabCell=getChildren(table.transform)[0];
			// prefabCell.parent=null;
			poolAdd(prefabCell);

			foreach(Transform e in table.transform){
				Destroy(e.gameObject);
			}			
		}
		if(!table)table=GetComponent<UITable>();
	}
	[SerializeField]
	protected Transform prefabCell;
}
