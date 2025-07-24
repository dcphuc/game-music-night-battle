using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPunchScaleEffect : MonoBehaviour 
{	
	public Vector3 originScale = new Vector3(1,1,1);
    public Vector3 punch = new Vector3(0.1f, 0.1f, 1);
    public float time = 0.5f;

    Tweener _tween;

    void OnEnable()
	{
        if(_tween != null) {
            _tween.Kill();
            _tween = null;
        }
        transform.localScale = originScale;
        _tween = transform.DOPunchScale(punch, time, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

	void OnDisable()
	{
        transform.localScale = originScale;
		if (_tween != null) {
			_tween.Kill ();
			_tween = null;
		}	
	}
}
