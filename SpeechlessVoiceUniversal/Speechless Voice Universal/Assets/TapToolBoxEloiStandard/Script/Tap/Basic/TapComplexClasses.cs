


public class TapComboSequence
{
    public TapValue[] m_tapValueSequence;

    public TapComboSequence(params TapValue[] sequentialValues)
    {
        m_tapValueSequence = sequentialValues;
    }

    public TapComboSequence(params TapCombo[] sequentialCombos)
    {
        TapValue[] newValues = new TapValue[sequentialCombos.Length];
        for (int i = 0; i < sequentialCombos.Length; i++)
        {
            newValues[i] = new TapValue(sequentialCombos[i]);
        }
        m_tapValueSequence = newValues;
    }

}
public class HandedTapComboSequence
{
    public HandTapValue[] m_tapValueSequence;

    public HandedTapComboSequence(params HandTapValue[] sequentialValues)
    {
        m_tapValueSequence = sequentialValues;
    }
}