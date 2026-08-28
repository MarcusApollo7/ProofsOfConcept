using System;
using System.Collections;
using System.Collections.Generic;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

// A Party is a group of characters which take their turns at the same time
// It holds a list of its members and tracks if everyone in the Party is downed

public class Party : IEnumerable
{
    // Properties
    private readonly List<ICombatant> Members;
    private bool EveryoneDowned = false;
    // Constructors
    public Party()
    {
        Members = [];
        
    }
    public Party(List<ICombatant> members)
    {
        Members = members;
    }
    // Methods
    public bool IsEveryoneDowned()
    {
        return EveryoneDowned;
    }
    public static Party operator +(Party a, Party b)
    {
        return new([.. a.Members, .. b.Members]); // returns a new party with all members
    }
    // Private nested class + GetEnumerator()
    private class MyEnumerator: IEnumerator
    {
        public List<ICombatant> members;
        int position = -1;

        public MyEnumerator(List<ICombatant> list)
            {
                members=list;
            }
            private IEnumerator getEnumerator()
            {
                return (IEnumerator)this;
            }
            //IEnumerator
            public bool MoveNext()
            {
                position++;
                return (position < members.Count);
            }
            //IEnumerator
            public void Reset()
            {
                position = -1;
            }
            //IEnumerator
            public object Current
            {
                get
                {
                    try
                    {
                        return members[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }  //end nested class
    public IEnumerator GetEnumerator()
    {
        return new MyEnumerator(Members);
    }
}
