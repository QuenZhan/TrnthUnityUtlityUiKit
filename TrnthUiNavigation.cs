using UnityEngine;
using System.Collections.Generic;

public class TrnthUiNavigation : TrnthPoolBase {
	static public TrnthUiNavigation main;
	public GameObject firstView;
	public TweenPosition prefabTweenerUpper;
	public TweenPosition prefabTweenerLower;
	public TweenAlpha prefabTweenerAlpha;
	public List<GameObject> list;
	public void push(GameObject prefab){
		if(list.Count>0){
			var last=list[list.Count-1];
			copyAndPlay(last.GetComponent<TweenPosition>(),prefabTweenerLower,true);
			copyAndPlay(last.GetComponent<TweenAlpha>(),prefabTweenerAlpha,true);
		}
		GameObject e=Spawn(prefab);
		list.Add(e);
		var tweener=e.GetComponent<TweenPosition>();
		if(!tweener)tweener=e.AddComponent<TweenPosition>();
		copyAndPlay(tweener,prefabTweenerUpper,true);
		if(!e.GetComponent<TweenAlpha>())e.AddComponent<TweenAlpha>();
	}
	public void pop(){
		if(list.Count<2)return;
		var last=list[list.Count-1];
		list.Remove(last);
		var tweener=last.GetComponent<TweenPosition>();
		copyAndPlay(tweener,prefabTweenerUpper,false);
		
		Despawn(last.transform,tweener.duration);
		if(list.Count<1)return;
		last=list[list.Count-1];
		copyAndPlay(last.GetComponent<TweenPosition>(),prefabTweenerLower,false);
		copyAndPlay(last.GetComponent<TweenAlpha>(),prefabTweenerAlpha,false);

	}
	public override void Awake(){
		base.Awake();
		main=this;
	}
	void copyAndPlay(TweenAlpha tweener,TweenAlpha prefabTweener,bool push){
		if(push){
			tweener.from=prefabTweener.from;
			tweener.to=prefabTweener.to;
		}else{
			tweener.from=prefabTweener.to;
			tweener.to=prefabTweener.from;
		}
		play(tweener,prefabTweener);
	}
	void copyAndPlay(TweenPosition tweener,TweenPosition prefabTweener,bool push){
		if(push){
			tweener.from=prefabTweener.from;
			tweener.to=prefabTweener.to;
		}else{
			tweener.from=prefabTweener.to;
			tweener.to=prefabTweener.from;
		}
		play(tweener,prefabTweener);
	}
	void play(UITweener tweener,UITweener prefab){
		tweener.animationCurve=prefab.animationCurve;
		tweener.duration=prefab.duration;
		tweener.delay=prefab.delay;
		tweener.ResetToBeginning();
		tweener.PlayForward();
	}
	void Start(){
		push(firstView);
	}
}