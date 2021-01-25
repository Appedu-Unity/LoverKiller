// This script adds a command to get the mouse position in a [Fungus](http://fungusgames.com/) flowchart.
//
// https://bitbucket.org/pierrerossel/getmouseposition
//
// 2018-05-02  Pierre Rossel  Initial version

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo (
	"Input", 
	"Get Mouse Position", 
	"Get the mouse position into variables"
)]
[AddComponentMenu("")]
public class GetMousePosition : Command
{

	[Tooltip("Variable to store the value of the horizontal mouse position")]
	[SerializeField]
	[VariableProperty (
		typeof(IntegerVariable), 
		typeof(FloatVariable),
		typeof(Vector3Variable)
	)]
	protected Variable storeMouseXTo;

	[Tooltip("Variable to store the value of the vertical mouse position")]
	[SerializeField]
	[VariableProperty (
		typeof(IntegerVariable), 
		typeof(FloatVariable),
		typeof(Vector3Variable)
	)]
	protected Variable storeMouseYTo;

	[Tooltip("By default, the mouse position is in pixels from bottom left, in screenspace. When converted to world space, it is converted to world units. Works best with orthographic camera.")]
	public BooleanData convertToWorldSpace;

	public override void OnEnter ()
	{
		//Debug.Log (Input.mousePosition);

		float x = Input.mousePosition.x;
		float y = Input.mousePosition.y;

		if (convertToWorldSpace.Value) {
			Vector3 vec = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, Camera.main.nearClipPlane));
			x = vec.x;
			y = vec.y;
		}

		doSetValue (x, storeMouseXTo, 0);
		doSetValue (y, storeMouseYTo, 1);

		Continue ();
	}

	protected void doSetValue (float value, Variable target, int vecPos)
	{
		if (target == null) {
			// Ignore
		} else if (target.GetType () == typeof(IntegerVariable)) {
			(target as IntegerVariable).Value = (int)value;
		} else if (target.GetType () == typeof(FloatVariable)) {
			(target as FloatVariable).Value = value;
		} else if (target.GetType () == typeof(Vector3Variable)) {
			Vector3 vec = (target as Vector3Variable).Value;
			vec [vecPos] = value;
			(target as Vector3Variable).Value = vec;
		}   
	}

	public override string GetSummary ()
	{
		return 
			(storeMouseXTo == null ? "" : storeMouseXTo.Key) + " " +
		(storeMouseYTo == null ? "" : storeMouseYTo.Key) +
		(convertToWorldSpace ? " (world space)" : "");
	}

	public override Color GetButtonColor ()
	{
		return new Color32(235, 191, 217, 255);
	}

	public override bool HasReference(Variable variable)
	{
		return (variable == this.storeMouseXTo || variable == this.storeMouseYTo || variable == this.convertToWorldSpace.booleanRef);
	}
}
