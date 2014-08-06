using UnityEngine;
using System;
using System.Collections.Generic;

public class TrnthUiNavigation : TrnthPoolBase {
	static public TrnthUiNavigation main;
	public GameObject firstView;
	public TweenPosition prefabTweenerUpper;
	public TweenPosition prefabTweenerLower;
	public TweenAlpha prefabTweenerAlpha;
	public List<GameObject> list;
	public void push(GameObject prefab,TrnthUiNavigationButtonPush pusher){
		GameObject e=Spawn(prefab);
		if(!e)return;
		if(list.Count>0){
			var last=list[list.Count-1];
			copyAndPlay(last.GetComponent<TweenPosition>(),prefabTweenerLower,true);
			copyAndPlay(last.GetComponent<TweenAlpha>(),prefabTweenerAlpha,true);
		}
		e.BroadcastMessage("onNavigationAppear",SendMessageOptions.DontRequireReceiver);
		if(pusher&&pusher.onAfterSapwn!=null)pusher.onAfterSapwn(e);
		list.Add(e);
		var tweener=e.GetComponent<TweenPosition>();
		if(!tweener)tweener=e.AddComponent<TweenPosition>();
		copyAndPlay(tweener,prefabTweenerUpper,true);
		if(!e.GetComponent<TweenAlpha>())e.AddComponent<TweenAlpha>();
		copyAndPlay(e.GetComponent<TweenAlpha>(),prefabTweenerAlpha,false);
	}
	public void pop(){
		if(list.Count<2)return;
		var last=list[list.Count-1];
		list.RemoveAt(list.Count-1);
		var tweener=last.GetComponent<TweenPosition>();
		copyAndPlay(tweener,prefabTweenerUpper,false);		
		// copyAndPlay(last.GetComponent<TweenAlpha>(),prefabTweenerAlpha,false);

		Despawn(last.transform,tweener.duration);
		if(list.Count<1)return;
		last=list[list.Count-1];
		last.BroadcastMessage("onNavigationAppear",SendMessageOptions.DontRequireReceiver);
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
		push(firstView,null);
	}
}