using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TapUtility : MonoBehaviour {


    public static TapValue GetTapBasedOnTapWithUsStandard(char character)
    {
        CharacterToTapWithUsStandard[] valuesFound = TapWithUsStandard.Where(k => k.m_character == character).OrderBy(k=>k.m_count).ToArray();
        if (valuesFound.Length > 0)
            return new TapValue(valuesFound[0].m_tapvalue.GetTapCombo());
        else
            return new TapValue( TapCombo.T_____);
        
    }

    
    public static HandTapValue GetTapBasedOnEloiStandard(char character)
    {
        CharacterToEloiStandard[] valuesFound= EloiStandard.Where(k => k.m_character == character).ToArray();
        if(valuesFound.Length>0)
            return new HandTapValue(valuesFound[0].m_handType, valuesFound[0].m_tapValue.GetTapCombo());
        else
            return new HandTapValue(HandType.Left, TapCombo.T_____);
    }

    internal static void GetTapValueFrom(bool [] handsBoolState, out TapValue lastTapLeft, out TapValue lastTapRight)
    {
        char [] leftHand = new char[5];
        char [] rightHand = new char[5];
        GetTapValueAsStringFrom(handsBoolState, out leftHand, out rightHand);
        lastTapLeft= GetTapValueFrom(leftHand) ;
        lastTapRight= GetTapValueFrom(rightHand);
    }

    internal static HandsTapValue ConvertToHandsValue(HandType handType,  TapValue value)
    {
        if (handType == HandType.Right)
            return new HandsTapValue(TapCombo.T_____, value.GetTapCombo());
        else
            return new HandsTapValue(value.GetTapCombo(), TapCombo.T_____);
    }
    internal static HandsTapValue ConvertToHandsValue(HandTapValue value)
    {
        if (value.m_handType == HandType.Right)
        {
            return new HandsTapValue(TapCombo.T_____, value.GetTapCombo());

        }
        else {
            return new HandsTapValue(value.GetTapCombo(), TapCombo.T_____);
        }
    }

    private static TapValue GetTapValueFrom(char[] leftHand)
    {
        foreach (TapCombo item in Enum.GetValues(typeof(TapCombo)).Cast<TapCombo>())
        {
            string itemString = item.ToString().Substring(1);
           
            if(itemString== new string(leftHand))
            {

                return new TapValue(item);
            }
        }
        return null;
    }

    private static void GetTapValueAsStringFrom(bool[] handsBoolState, out char[] leftHand, out char[] rightHand)
    {
        leftHand = new char[5];
        rightHand = new char[5];
        for (int i = 0; i < 10; i++)
        {
            if(i<5)
              leftHand[i] = handsBoolState[i] ? 'O' : '_';
            else
              rightHand[i-5] = handsBoolState[i] ? 'O' : '_';
        }
    }

    public struct CharacterToTapWithUsStandard
    {
        public CharacterToTapWithUsStandard(char character, TapCombo combo, int count = 1, string description="")
        {
            m_character = character;
            m_tapvalue = new TapValue(combo);
            m_count = count;
            if (string.IsNullOrEmpty(description))
                m_description = "" + m_character;
            else m_description = description;
        }
        public string m_description;
        public char m_character;
        public TapValue m_tapvalue;
        public int m_count;


    }


    public static CharacterToTapWithUsStandard[] TapWithUsForbiden = new CharacterToTapWithUsStandard[] {

        new CharacterToTapWithUsStandard(' ',TapCombo.TOOO__, 1,"Shift"),
        new CharacterToTapWithUsStandard(' ',TapCombo.T__OOO, 1,"Switch"),
        new CharacterToTapWithUsStandard(' ',TapCombo.TOOO__, 2,"Shift"),
        new CharacterToTapWithUsStandard(' ',TapCombo.T__OOO, 2,"Switch"),
        new CharacterToTapWithUsStandard(' ',TapCombo.TOOO__, 3,"Shift"),
        new CharacterToTapWithUsStandard(' ',TapCombo.T__OOO, 3,"Switch"),

        new CharacterToTapWithUsStandard(' ',TapCombo.TOO___, 2,"Volume UP"),
        new CharacterToTapWithUsStandard(' ',TapCombo.TOO_O_, 2,"Voice Over"),
        new CharacterToTapWithUsStandard(' ',TapCombo.T_O_OO, 2,"Toogle Layouts"),
        new CharacterToTapWithUsStandard(' ',TapCombo.T___O_, 3,"Sleep"),
        new CharacterToTapWithUsStandard(' ',TapCombo.TOO___, 3,"Awake"),
        new CharacterToTapWithUsStandard(' ',TapCombo.TOO_O_, 3,"Voice Over"),
        new CharacterToTapWithUsStandard(' ',TapCombo.T_O_OO, 3,"Toogle Layouts"),
        new CharacterToTapWithUsStandard(' ',TapCombo.T_OOOO, 3,"Show/Hide Keyboard")
    };

    public static CharacterToTapWithUsStandard[] TapWithUsStandard = new CharacterToTapWithUsStandard[] {

        new CharacterToTapWithUsStandard('a',TapCombo.TO____, 1),
        new CharacterToTapWithUsStandard('v',TapCombo.TO____, 2),
        new CharacterToTapWithUsStandard('@',TapCombo.TO____, 3),

        new CharacterToTapWithUsStandard('e',TapCombo.T_O___, 1),
        new CharacterToTapWithUsStandard('!',TapCombo.T_O___, 2),
        new CharacterToTapWithUsStandard('=',TapCombo.T_O___, 3),

        new CharacterToTapWithUsStandard('i',TapCombo.T__O__, 1),
        new CharacterToTapWithUsStandard('j',TapCombo.T__O__, 2),

        new CharacterToTapWithUsStandard('o',TapCombo.T___O_, 1),
        new CharacterToTapWithUsStandard('q',TapCombo.T___O_, 2),

        new CharacterToTapWithUsStandard('u',TapCombo.T____O, 1),
        new CharacterToTapWithUsStandard('w',TapCombo.T____O, 2),
        new CharacterToTapWithUsStandard('_',TapCombo.T____O, 3),

        new CharacterToTapWithUsStandard('s',TapCombo.T___OO, 1),


        new CharacterToTapWithUsStandard('l',TapCombo.T__OO_, 1),
        new CharacterToTapWithUsStandard('/',TapCombo.T__OO_, 2),
        new CharacterToTapWithUsStandard('+',TapCombo.T__OO_, 3),


        new CharacterToTapWithUsStandard('t',TapCombo.T_OO__, 1),
        new CharacterToTapWithUsStandard('"',TapCombo.T_OO__, 2),
        new CharacterToTapWithUsStandard('#',TapCombo.T_OO__, 3),

        new CharacterToTapWithUsStandard('n',TapCombo.TOO___, 1),


        new CharacterToTapWithUsStandard('z',TapCombo.T__O_O, 1),

        new CharacterToTapWithUsStandard('m',TapCombo.T_O_O_, 1),
        new CharacterToTapWithUsStandard(',',TapCombo.T_O_O_, 2),
        
        new CharacterToTapWithUsStandard('d',TapCombo.TO_O__, 1),
        new CharacterToTapWithUsStandard('&',TapCombo.TO_O__, 2),
        new CharacterToTapWithUsStandard('$',TapCombo.TO_O__, 3),

        new CharacterToTapWithUsStandard('b',TapCombo.T_O__O, 1),
        new CharacterToTapWithUsStandard('[',TapCombo.T_O__O, 2),
        new CharacterToTapWithUsStandard(']',TapCombo.T_O__O, 3),

        new CharacterToTapWithUsStandard('k',TapCombo.TO__O_, 1),
        new CharacterToTapWithUsStandard('?',TapCombo.TO__O_, 2),
        new CharacterToTapWithUsStandard('*',TapCombo.TO__O_, 3),

        new CharacterToTapWithUsStandard('y',TapCombo.TO___O, 1),
        new CharacterToTapWithUsStandard('z',TapCombo.TO___O, 2),


       // new CharacterToTapWithUsStandard(char.Parse("DEL"),TapCombo.T_OOO_, 2),



        new CharacterToTapWithUsStandard('x',TapCombo.T_O_OO, 1),
        
        new CharacterToTapWithUsStandard('q',TapCombo.T_OO_O, 1),

        new CharacterToTapWithUsStandard('\r',TapCombo.TO__OO, 1),
        new CharacterToTapWithUsStandard('\r',TapCombo.TO__OO, 2),
        new CharacterToTapWithUsStandard('\r',TapCombo.TO__OO, 3),
        new CharacterToTapWithUsStandard('\n',TapCombo.TO__OO, 1),
        new CharacterToTapWithUsStandard('\n',TapCombo.TO__OO, 2),
        new CharacterToTapWithUsStandard('\n',TapCombo.TO__OO, 3),

        new CharacterToTapWithUsStandard('f',TapCombo.TOO_O_, 1),

        new CharacterToTapWithUsStandard('r',TapCombo.TO_OO_, 1),
        new CharacterToTapWithUsStandard('>',TapCombo.TO_OO_, 2),
        new CharacterToTapWithUsStandard('<',TapCombo.TO_OO_, 3),

        new CharacterToTapWithUsStandard('p',TapCombo.TOO__O, 1),
        new CharacterToTapWithUsStandard('(',TapCombo.TOO__O, 2),
        new CharacterToTapWithUsStandard(')',TapCombo.TOO__O, 3),

        new CharacterToTapWithUsStandard('w',TapCombo.TO_O_O, 1),

        new CharacterToTapWithUsStandard('r',TapCombo.TOOOO_, 1),
        new CharacterToTapWithUsStandard('\'',TapCombo.TOOOO_, 2),
        new CharacterToTapWithUsStandard('%',TapCombo.TOOOO_, 3),

         new CharacterToTapWithUsStandard('j',TapCombo.TOOO_O, 1),
        new CharacterToTapWithUsStandard('v',TapCombo.TOO_OO, 1),
     
         new CharacterToTapWithUsStandard('c',TapCombo.TO_OOO, 1),
        new CharacterToTapWithUsStandard(':',TapCombo.TO_OOO, 2),
        new CharacterToTapWithUsStandard(';',TapCombo.TO_OOO, 3),

         new CharacterToTapWithUsStandard('h',TapCombo.T_OOOO, 1),
        new CharacterToTapWithUsStandard('-',TapCombo.T_OOOO, 2),
        
         new CharacterToTapWithUsStandard(' ',TapCombo.TOOOOO, 1),
        new CharacterToTapWithUsStandard('.',TapCombo.TOOOOO, 2),
       
        //DELETE _OOO_
        //DELETE OOO__
        //DELETE __OOO



        new CharacterToTapWithUsStandard('0',TapCombo.T_OOOO, 1),
        new CharacterToTapWithUsStandard('1',TapCombo.TO____, 1),
        new CharacterToTapWithUsStandard('2',TapCombo.T_O___, 1),
        new CharacterToTapWithUsStandard('3',TapCombo.T__O__, 1),
        new CharacterToTapWithUsStandard('4',TapCombo.T___O_, 1),
        new CharacterToTapWithUsStandard('5',TapCombo.T____O, 1),
        new CharacterToTapWithUsStandard('6',TapCombo.TO___O, 1),
        new CharacterToTapWithUsStandard('7',TapCombo.T_O__O, 1),
        new CharacterToTapWithUsStandard('8',TapCombo.T__O_O, 1),
        new CharacterToTapWithUsStandard('9',TapCombo.T___OO, 1)

        
        
        //new CharacterToTapWithUsStandard('.',TapCombo.T09O_O__, 2),
        //new CharacterToTapWithUsStandard('\t',TapCombo.T29OOOO_, 3),

        
    };


    public struct CharacterToEloiStandard {
        public CharacterToEloiStandard(char character, HandType hand, int id): this(character, hand, (TapCombo) id)
        {
        }
        public CharacterToEloiStandard(char character, HandType hand, TapCombo combo) {
            m_character = character;
            m_handType = hand;
            m_tapValue = new TapValue(combo);
        }
        public char m_character;
        public HandType m_handType;
        public TapValue m_tapValue;

    }
    public static CharacterToEloiStandard[] EloiStandard = new CharacterToEloiStandard[] {
        new CharacterToEloiStandard('0', HandType.Left, TapCombo.TO____),
        new CharacterToEloiStandard('1', HandType.Left, TapCombo.T_O___),
        new CharacterToEloiStandard('2', HandType.Left, TapCombo.T__O__),
        new CharacterToEloiStandard('3', HandType.Left, TapCombo.T___O_),
        new CharacterToEloiStandard('4', HandType.Left, TapCombo.T____O),
        new CharacterToEloiStandard('a', HandType.Left, TapCombo.T___OO),
        new CharacterToEloiStandard('b', HandType.Left, TapCombo.T__OO_),
        new CharacterToEloiStandard('c', HandType.Left, TapCombo.T_OO__),
        new CharacterToEloiStandard('d', HandType.Left, TapCombo.TOO___),
        new CharacterToEloiStandard('e', HandType.Left, TapCombo.T__O_O),
        new CharacterToEloiStandard('f', HandType.Left, TapCombo.T_O_O_),
        new CharacterToEloiStandard('g', HandType.Left, TapCombo.TO_O__),
        new CharacterToEloiStandard('h', HandType.Left, TapCombo.T_O__O),
        new CharacterToEloiStandard('i', HandType.Left, TapCombo.TO__O_),
        new CharacterToEloiStandard('j', HandType.Left, TapCombo.TO___O),
        //new CharacterToEloiStandard('y', HandType.Left, TapCombo.T__OOO),
        new CharacterToEloiStandard('k', HandType.Left, TapCombo.T_OOO_),
        //new CharacterToEloiStandard('z', HandType.Left, TapCombo.TOOO__),
        new CharacterToEloiStandard('l', HandType.Left, TapCombo.T_O_OO),
        new CharacterToEloiStandard('m', HandType.Left, TapCombo.T_OO_O),
        new CharacterToEloiStandard('n', HandType.Left, TapCombo.TOO__O),
        new CharacterToEloiStandard('o', HandType.Left, TapCombo.TOO_O_),
        new CharacterToEloiStandard('p', HandType.Left, TapCombo.TO_OO_),
        new CharacterToEloiStandard('q', HandType.Left, TapCombo.TO__OO),
        new CharacterToEloiStandard('r', HandType.Left, TapCombo.TO_O_O),
        new CharacterToEloiStandard('s', HandType.Left, TapCombo.TOOOO_),
        new CharacterToEloiStandard('t', HandType.Left, TapCombo.TOOO_O),
        new CharacterToEloiStandard('u', HandType.Left, TapCombo.TOO_OO),
        new CharacterToEloiStandard('v', HandType.Left, TapCombo.TO_OOO),
        new CharacterToEloiStandard('w', HandType.Left, TapCombo.T_OOOO),
        new CharacterToEloiStandard('x', HandType.Left, TapCombo.TOOOOO),

        new CharacterToEloiStandard('5', HandType.Right, TapCombo.TO____),
        new CharacterToEloiStandard('6', HandType.Right, TapCombo.T_O___),
        new CharacterToEloiStandard('7', HandType.Right, TapCombo.T__O__),
        new CharacterToEloiStandard('8', HandType.Right, TapCombo.T___O_),
        new CharacterToEloiStandard('9', HandType.Right, TapCombo.T____O),
        new CharacterToEloiStandard('A', HandType.Right, TapCombo.TOO___),
        new CharacterToEloiStandard('B', HandType.Right, TapCombo.T_OO__),
        new CharacterToEloiStandard('C', HandType.Right, TapCombo.T__OO_),
        new CharacterToEloiStandard('D', HandType.Right, TapCombo.T___OO),
        new CharacterToEloiStandard('E', HandType.Right, TapCombo.TO_O__),
        new CharacterToEloiStandard('F', HandType.Right, TapCombo.T_O_O_),
        new CharacterToEloiStandard('G', HandType.Right, TapCombo.T__O_O),
        new CharacterToEloiStandard('H', HandType.Right, TapCombo.TO__O_),
        new CharacterToEloiStandard('I', HandType.Right, TapCombo.T_O__O),
        new CharacterToEloiStandard('J', HandType.Right, TapCombo.TO___O),
   //     new CharacterToEloiStandard('Y', HandType.Right, TapCombo.T__OOO),
        new CharacterToEloiStandard('K', HandType.Right, TapCombo.T_OOO_),
   //     new CharacterToEloiStandard('Z', HandType.Right, TapCombo.TOOO__),
        new CharacterToEloiStandard('L', HandType.Right, TapCombo.TOO_O_),
        new CharacterToEloiStandard('M', HandType.Right, TapCombo.TO_OO_),
        new CharacterToEloiStandard('N', HandType.Right, TapCombo.TOO__O),
        new CharacterToEloiStandard('O', HandType.Right, TapCombo.T_O_OO),
        new CharacterToEloiStandard('P', HandType.Right, TapCombo.T_OO_O),
        new CharacterToEloiStandard('Q', HandType.Right, TapCombo.TO__OO),
        new CharacterToEloiStandard('R', HandType.Right, TapCombo.TO_O_O),
        new CharacterToEloiStandard('S', HandType.Right, TapCombo.T_OOOO),
        new CharacterToEloiStandard('T', HandType.Right, TapCombo.TO_OOO),
        new CharacterToEloiStandard('U', HandType.Right, TapCombo.TOO_OO),
        new CharacterToEloiStandard('V', HandType.Right, TapCombo.TOOO_O),
        new CharacterToEloiStandard('W', HandType.Right, TapCombo.TOOOO_),
        new CharacterToEloiStandard('X', HandType.Right, TapCombo.TOOOOO)
    };
}


