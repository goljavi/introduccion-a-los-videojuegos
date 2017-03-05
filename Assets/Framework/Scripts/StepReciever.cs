using UnityEngine;
using System.Collections;

public class StepReciever : MonoBehaviour {

    public Player p;

	public void Step()
    {
        p.Step();
    }
}
