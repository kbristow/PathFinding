using UnityEngine;
using System.Collections.Generic;

//Handles moving along a path of targets
public class MoveToTarget : MonoBehaviour {

	private List<Vector3> targets = new List<Vector3>();
	public List<Vector3> Targets {
		get{ return targets;}
		set{ this.targets = value;}
	}

	private Vector3 currentGoal;
	private bool hasGoal = false;
	
	private Vector3 startMarker;

	
	// Time when the movement started.
	private float startTime;
	
	// Total distance between the markers.
	private float journeyLength;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		CharacterSettings charSettings = (gameObject.GetComponent("CharacterSettings") as CharacterSettings);

		if(charSettings == null || !charSettings.canMove){
			return;
		}
		if (!hasGoal && Targets.Count > 0) {
			currentGoal = Targets[0];
			Targets.RemoveAt(0);
			// Keep a note of the time the movement started.
			startTime = Time.time;

			startMarker = transform.position;

			// Calculate the journey length.
			journeyLength = Vector3.Distance(startMarker, currentGoal);
			hasGoal = true;
		}

		if (hasGoal) {
			// Distance moved = time * speed.
			float distCovered = (Time.time - startTime) * charSettings.movementSpeed;
			
			// Fraction of journey completed = current distance divided by total distance.
			float fracJourney = distCovered / journeyLength;
			
			// Set our position as a fraction of the distance between the markers.
			Vector3 newPosition = Vector3.Lerp(startMarker, currentGoal, fracJourney) + Vector3.zero;
			if(newPosition != Vector3.zero){
				transform.position = newPosition;
			}

			if (transform.position == currentGoal){
				hasGoal = false;
			}
		}
	}

	public void setTargetsFromArray(Vector3 [] targetsArr){
		Targets = new List<Vector3> ();
		foreach (Vector3 target in targetsArr) {
			Targets.Add(target);
		}
		hasGoal = false;
		startMarker = transform.position;
		// Keep a note of the time the movement started.
		startTime = Time.time;
	}

}