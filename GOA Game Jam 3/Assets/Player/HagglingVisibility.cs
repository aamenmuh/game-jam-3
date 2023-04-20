using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HagglingVisibility : MonoBehaviour
{
    private bool _visible = false;
    private SpriteRenderer _sp;
    private SpriteRenderer _spChild;
    // Update is called once per frame
    void Start(){
    	_sp = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
        	if(_visible){
        		_visible = false;
        	}else{
        		_visible = true;
        	}
        }
        if(_visible){
        	_sp.enabled = true;
        }else{
        	_sp.enabled = false;
        }
    }
}
