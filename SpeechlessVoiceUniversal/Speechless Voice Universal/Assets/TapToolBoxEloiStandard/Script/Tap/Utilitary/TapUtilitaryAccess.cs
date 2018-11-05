using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapUtilitaryAccess : MonoBehaviour {


    public void SetHandToLeft() { TapUtilitary.SetHandTo(HandType.Left); }
    public void SetHandToRight() { TapUtilitary.SetHandTo(HandType.Right); }

}
