using System;
using System.Collections;
using System.Collections.Generic;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class Party : IEnumerable
{
    // Properties
    private List<ICombatant> members;
    private bool EveryoneDowned;
    // Constructors
    public Party()
    {
        members = [];
        EveryoneDowned = false;
    }
    // Method
    public bool IsEveryoneDowned()
    {
        return EveryoneDowned;
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
        return new MyEnumerator(members);
    }
}
