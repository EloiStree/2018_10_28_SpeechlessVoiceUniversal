




using System;
using UnityEngine;


[System.Serializable]
public class HandsTapValue {    

    [SerializeField]
    private TapValue m_leftCombo;
    [SerializeField]
    private TapValue m_rightCombo;



    public HandsTapValue(): this(TapCombo.T_____, TapCombo.T_____)
    {}


    public HandsTapValue(TapCombo left, TapCombo right) {
        m_leftCombo = new TapValue(left);
        m_rightCombo = new TapValue(right);
    }

    internal TapValue GetRightValue()
    {
        return m_rightCombo;
    }

    internal TapValue GetLeftValue()
    {
        return m_leftCombo;
    }

    public HandTapValue GetHand(HandType handType) {
        if(handType==HandType.Left)
            return new HandTapValue(handType, m_leftCombo.GetTapCombo());
        else
            return new HandTapValue(handType, m_rightCombo.GetTapCombo());
    }

    public TapValue GetTapValue()
    {
        if (m_leftCombo.HasFingersPressed() && m_rightCombo.HasFingersPressed())
            return new TapValue(TapCombo.T_____);
        if (!m_leftCombo.HasFingersPressed() && !m_rightCombo.HasFingersPressed())
            return new TapValue(TapCombo.T_____);
        if (m_leftCombo.HasFingersPressed())
            return new HandTapValue(HandType.Left, m_leftCombo.GetTapCombo());
        if (m_rightCombo.HasFingersPressed())
            return new HandTapValue(HandType.Right, m_rightCombo.GetTapCombo());
        return new TapValue(TapCombo.T_____);
    }

    public TapValue GetTapAppendValue()
    {
        TapValue val = new TapValue(TapCombo.T_____);
        val.Append(m_leftCombo);
        val.Append(m_rightCombo);
        return val;
    }


    public void Append(HandsTapValue value) {

        if (value == null)
            return;

        bool[] fingersCreated = value.GetHandsState();
        bool[] fingers = GetHandsState();
        for (int i = 0; i < 10; i++)
        {
            fingers[i] = fingers[i] || fingersCreated[i];
        }
        Set(fingers);
    }
    internal void Append(HandTapValue value)
    {
        Append(TapUtility.ConvertToHandsValue(value));
        
    }
    internal void Append(TapValue value)
    {
        TapValue inverseValue = new TapValue( value.GetTapCombo());
        inverseValue.Inverse();
        Append(TapUtility.ConvertToHandsValue(HandType.Left, inverseValue));
        Append(TapUtility.ConvertToHandsValue(HandType.Right, value));
    }

    internal HandTapValue GetHandTapValue()
    {
        if (m_leftCombo.HasFingersPressed() && m_rightCombo.HasFingersPressed())
            return null;
        if (!m_leftCombo.HasFingersPressed() && !m_rightCombo.HasFingersPressed())
            return null;
        if (m_leftCombo.HasFingersPressed() )
            return new HandTapValue(HandType.Left, m_leftCombo.GetTapCombo());
        if ( m_rightCombo.HasFingersPressed())
            return new HandTapValue(HandType.Right, m_rightCombo.GetTapCombo());
        return null;

    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        HandsTapValue p = (HandsTapValue)obj;
        return AreEquals(p, this);
    }

    public override int GetHashCode()
    {
        return -1184375416 + m_leftCombo.GetHashCode() + m_rightCombo.GetHashCode()  ;
    }

    public override string ToString()
    {
        return m_leftCombo.GetDescription() + " "+ m_rightCombo.GetDescription();
    }
    public static bool AreEquals(HandsTapValue a, HandsTapValue b)
    {
        if (a == null || b == null)
            return false;
        return ((a.m_leftCombo.GetTapCombo() == b.m_leftCombo.GetTapCombo()) && (a.m_rightCombo.GetTapCombo() == b.m_rightCombo.GetTapCombo()));
    }


    public bool IsDown(FingerIndex fingerIndex, bool ignoreHandSide = false)
    {
        bool[] handsState = GetHandsState();

        int index = (int) fingerIndex;
        int ignoredSideIndex = index > 4 ? index - 5 : index;
        if (ignoreHandSide)
        {
            switch (fingerIndex)
            {
                case FingerIndex.LeftPinky:
                case FingerIndex.RighPinky:
                    return handsState[(int)FingerIndex.LeftPinky] || handsState[(int)FingerIndex.RighPinky];
                case FingerIndex.LeftMiddle:
                case FingerIndex.RightMiddle:
                    return handsState[(int)FingerIndex.LeftMiddle] || handsState[(int)FingerIndex.RightMiddle];
                case FingerIndex.leftThumb:
                case FingerIndex.RightThumb:
                    return handsState[(int)FingerIndex.leftThumb] || handsState[(int)FingerIndex.RightThumb];
                case FingerIndex.LeftIndex:
                case FingerIndex.RightIndex:
                    return handsState[(int)FingerIndex.LeftIndex] || handsState[(int)FingerIndex.RightIndex];
                case FingerIndex.LeftRing:
                case FingerIndex.RightRing:
                    return handsState[(int)FingerIndex.LeftRing] || handsState[(int)FingerIndex.RightRing];
                default:
                    break;
            }
        }
        else
        {
            return handsState[index];

        }
        return false;
    }
    public void Add(HandsTapValue handsValue)
    {
        if (handsValue == null)
            return;
        m_leftCombo.Append(handsValue.m_leftCombo);
        m_rightCombo.Append(handsValue.m_rightCombo);

    }
    public void Add(HandTapValue handValue)
    {
        if (handValue == null)
            return;

        if (handValue.m_handType == HandType.Left)
            m_leftCombo.Append(handValue);
        else m_rightCombo.Append(handValue);
    }
    public void Add(TapValue handValue, HandType handType)
    {
        if (handValue == null)
            return;

        if (handType == HandType.Left)
            m_leftCombo.Append(handValue);
        else m_rightCombo.Append(handValue);
    }

    public void Set(bool [] fingerState)
    {
        if (fingerState == null)
            return;

        if (fingerState.Length != 10)
        {

            bool[] fingers = new bool[10];
            for (int i = 0; i < fingerState.Length && i < 10; i++)
            {
                fingers[i] = fingerState[i]; 
            }
            fingerState = fingers;
        }

        TapUtility.GetTapValueFrom(fingerState, out m_leftCombo, out m_rightCombo);
    }


    public void Set(HandsTapValue handsValue)
    {
        
        if (handsValue == null)
            return;


        m_leftCombo.SetWith(handsValue.m_leftCombo);
        m_rightCombo.SetWith(handsValue.m_rightCombo);
    }

    public bool HasFingerDown()
    {
        bool[] state = GetHandsState();
        for (int i = 0; i < 10; i++)
        {
            if (state[i])
                return true;
        }
        return false;
    }

    public void Set(HandTapValue handValue)
    {
        if (handValue == null)
            return;

        if (handValue.m_handType == HandType.Left)
            m_leftCombo.SetWith(handValue);
        else m_rightCombo.SetWith(handValue);
    }
    public void Set(TapValue handValue, HandType handType)
    {
        if(handValue==null)
            return;

        if (handType == HandType.Left)
            m_leftCombo.SetWith(handValue);
        else m_rightCombo.SetWith(handValue);

    }
    public bool[] GetHandsState()
    {
        bool[] handState = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            if (i<5)
                handState[i] 
                    = m_leftCombo.IsFingerActive(i);
            else
                handState[i]
                    = m_rightCombo.IsFingerActive(i-5);
        }
        return handState;
    }
    public bool[] GetHandState(HandType handType)
    {
        bool[] handState = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            if (handType == HandType.Left)
                handState[i] = m_leftCombo.IsFingerActive(i);
            else

                handState[i] = m_rightCombo.IsFingerActive(i);
        }
        return handState;
    }


    public void Clear()
    {
        // SUPER DIRTY
        /*
        if (m_rightCombo == null)
            m_rightCombo = new TapValue();
        if (m_leftCombo == null)
            m_leftCombo = new TapValue();
            */
        m_leftCombo.Clear();
        m_rightCombo.Clear();
    }

}

[System.Serializable]
public class HandTapValue : TapValue {

    public HandType m_handType;

    public HandTapValue(HandType hand, int id):base(id)
    {
        m_handType = hand;
    }
    public HandTapValue(HandType hand, TapCombo combo):base(combo)
    {
        m_handType = hand;
    }


    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        HandTapValue p = (HandTapValue)obj;
        return AreEquals(p, this);
    }
    public static bool AreEquals(HandTapValue a, HandTapValue b)
    {
        if (a == null || b == null)
            return false;
        return (a.GetTapCombo() == b.GetTapCombo()) && (a.m_handType == b.m_handType);
    }
    public override int GetHashCode()
    {
        return -1184375416 + m_handType.GetHashCode();
    }

    public override string ToString()
    {
        return m_handType + "-" + GetDescription();
    }
}

[System.Serializable]
public class TapValue
{
    public TapValue(int id)
    {
        m_combo = (TapCombo)id;
    }
    public TapValue(TapCombo combo)
    {
        m_combo = combo;
    }

    public TapValue()
    {
        m_combo = TapCombo.T_____;
    }

    [SerializeField]
    TapCombo m_combo;

    public TapCombo GetTapCombo() { return m_combo; }
    public  void SetTapCombo(TapCombo comboValue ) {  m_combo = comboValue; }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        TapValue p = (TapValue) obj;
        return AreEquals(p, this);
    }

    public static bool AreEquals(TapValue a, TapValue b) {
        if (a == null || b == null)
            return false;
        return a.m_combo == b.m_combo;
    }
 
    public override string ToString()
    {
        return m_combo.ToString();
    }

    public string GetDescription() {
        return m_combo.ToString().Substring(1);
    }

    public bool IsFingerActive(int index)
    {
        index = UnityEngine.Mathf.Clamp(index, 0, 4);
        string combo = GetDescription();
        return combo[index]!='_';
    }

    internal void Clear()
    {
        m_combo = TapCombo.T_____;
    }

    internal void SetWith(TapValue value)
    {
        if (value == null)
            return;
        m_combo = value.m_combo;
    }

    internal void Append(TapValue value)
    {
        if (value == null)
            return;

        bool [] current = GetFingersState();
        bool [] given = value.GetFingersState();
        bool [] result = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            result [i] = current[i] || given[i];
        }
    }

    private bool[] GetFingersState()
    {
        bool[] result = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            result[i] = IsFingerActive(i);
        }
        return result;
    }

    public void SetFingersState(bool[] state) {
        string strCombo = "T";
        for (int i = 0; i < 5; i++)
        {
            strCombo += state[i] ? 'O' : '_';
        }
        TapCombo combo = (TapCombo) Enum.Parse(typeof(TapCombo), strCombo);
        m_combo = combo;
    }

    internal bool HasFingersPressed()
    {
        return m_combo != TapCombo.T_____;
    }

    internal void Inverse()
    {
        bool[] current = GetFingersState();
        bool tmp = false;
        for (int i = 0; i < 3; i++)
        {
            tmp = current[i];
            current[i] = current[4 - i];
            current[4 - i] = tmp;
        }
        SetFingersState(current);
    }
}


public enum HandType { Left, Right }
public enum FingerIndex : int {
    LeftPinky=0,
    LeftRing = 1,
    LeftMiddle = 2,
    LeftIndex = 3,
    leftThumb = 4,
    RightThumb = 5,
    RightIndex = 6,
    RightMiddle = 7,
    RightRing = 8,
    RighPinky = 9
}
public enum TapCombo : int
{
    T_____ = -1,
    TO____ = 0,
    T_O___ = 1,
    T__O__ = 2,
    T___O_ = 3,
    T____O = 4,
    TOO___ = 5,
    T_OO__ = 6,
    T__OO_ = 7,
    T___OO = 8,
    TO_O__ = 9,
    T_O_O_ = 10,
    T__O_O = 11,
    TO__O_ = 12,
    T_O__O = 12,
    TO___O = 14,
    TOOO__ = 15,//Taken: Shift
    T_OOO_ = 16,
    T__OOO = 17,//Taken: Switch
    TOO_O_ = 18,
    TO_OO_ = 19,
    TOO__O = 20,
    T_O_OO = 21,
    T_OO_O = 22,
    TO__OO = 23,
    TO_O_O = 24,
    T_OOOO = 25,
    TO_OOO = 26,
    TOO_OO = 27,
    TOOO_O = 28,
    TOOOO_ = 29,
    TOOOOO = 30


}