using UnityEngine;
using System.Collections.Generic;

public class TrnthUiNavigation : TrnthPoolBase {
	static public TrnthUiNavigation main;
	public GameObject firstView;
	public TweenPosition prefabTweener;
	public List<GameObject> list;
	public void push(GameObject prefab){
		if(list.Count>0){
			var last=list[list.Count-1];
			last.SetActive(false);
		}
		GameObject e=Spawn(prefab);
		list.Add(e);
		var tweener=e.GetComponent<TweenPosition>();
		if(!tweener)tweener=e.AddComponent<TweenPosition>();
		// tweener.from=tra.localPosition+Vector3.right*700;
		// tweener.to=tra.localPosition;
		tweener.from=prefabTweener.from;
		tweener.to=prefabTweener.to;
		tweener.animationCurve=prefabTweener.animationCurve;
		tweener.duration=prefabTweener.duration;
		// e.transform.position=pos;
		tweener.PlayForward();
	}
	public void pop(){
		if(list.Count<1)return;
		var last=list[list.Count-1];
		list.Remove(last);
		var tweener=last.GetComponent<TweenPosition>();
		tweener.PlayReverse();
		Despawn(last.transform,tweener.duration);
		if(list.Count<1)return;
		last=list[list.Count-1];
		last.SetActive(true);
	}
	public override void Awake(){
		base.Awake();
		main=this;
	}
	void Start(){
		push(firstView);
	}
}