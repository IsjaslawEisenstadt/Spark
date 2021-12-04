using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShaderAdapter : MonoBehaviour
{
	public Material material;

	[SerializeField, Range(0, 1)] private float _cutOff;

	public float cutOff
	{
		get => _cutOff;
		set
		{
			material.SetFloat("_CutOff", value);
			_cutOff = value;
		}
	}

	void Update()
	{
		cutOff = _cutOff;
	}

	public void OnValidate()
	{
		cutOff = _cutOff;
	}
}
