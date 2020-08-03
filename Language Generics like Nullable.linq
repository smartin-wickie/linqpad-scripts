<Query Kind="Program">
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Diagnostics.Contracts</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Reflection</Namespace>
  <Namespace>System.Runtime</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Security</Namespace>
</Query>

void Main()
{
	var q = new LookupValue<int>(44);
	var r = new LookupValue<int>(44);
	var u = new LookupValue<int>();

	var s = new LookupValue<string>("44");
	var t = new LookupValue<string>();
	var v = new LookupValue<string>();
	t = s + "  sadads";
	
	(q.Value + r.Value).Dump();
	(s.Value + t.Value).Dump();

	q.Dump();
	r.Dump();
	u.Dump();
	s.Dump();
	t.Dump();
	v.Dump();
	
	LINQPad.Util.HorizontalRun(true, typeof(int) ,typeof(string)).Dump();
}

/* 
	Hacked from Nullable Type 
	Note:  Does not handle other Operators like + - etc.
	http://www.dotnetframework.org/default.aspx/4@0/4@0/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/clr/src/BCL/System/Nullable@cs/1305376/Nullable@cs
	
	This is terrible too
	There are nullable ref types coming in C#
	Don't do this
	Ancestors are watching you
*/
    [Serializable]
    public class LookupValue<T> where T : IConvertible
    {
        internal T value;
  
        public LookupValue(T value) {
            this.value = value; 
        }

	public LookupValue()
	{
		this.value = default(T);
	}


        public T Value {
            get {
                return value; 
            }
        }
 
        public T GetValueOrDefault() { 
            return value;
        } 
 
        public T GetValueOrDefault(T defaultValue) {
            return  value != null ? value : defaultValue;
        } 
 
        public override bool Equals(object other) { 
            if (value != null) return other == null; 
            if (other == null) return false;
            return value.Equals(other); 
        }
 
        public override int GetHashCode() {
            return value != null ? value.GetHashCode() : 0; 
        }
  
        public override string ToString() { 
            return value != null ? value.ToString() : "";
        } 
 
        public static implicit operator LookupValue<T>(T value) {
            return new LookupValue<T>(value);
        } 
 
        public static explicit operator T(LookupValue<T> value) { 
            return value.Value; 
        }
  
                 } 
        
    public static class LookupValue
    {
        public static int Compare<T>(LookupValue<T> n1, LookupValue<T> n2) where T : IConvertible
        {
            if (n1.value != null) { 
                if (n2.value != null) return Comparer<T>.Default.Compare(n1.value, n2.value); 
                return 1;
            } 
            if (n2.value != null) return -1;
                return 0;
            }
        
        public static bool Equals<T>(LookupValue<T> n1, LookupValue<T> n2) where T : IConvertible
        { 
            if (n1.value != null) {
                if (n2.value != null) return EqualityComparer<T>.Default.Equals(n1.value, n2.value); 
                return false;
                }
            if (n2.value != null) return false;
                    return true; 
                }

	// If the type provided is not a Genericizer Type, return null. 
	// Otherwise, returns the underlying type of the Genericizer type
	public static Type GetUnderlyingType(Type underType)
	{
		if ((object)underType == null)
		{
			throw new ArgumentNullException("underType");
		}
		Contract.EndContractBlock();
		Type result = null;
		if (underType.IsGenericType && !underType.IsGenericTypeDefinition)
		{
			// instantiated generic type only 
			Type genericType = underType.GetGenericTypeDefinition();
			if (Object.ReferenceEquals(genericType, typeof(LookupValue<>)))
			{
				result = underType.GetGenericArguments()[0];
			}
		}
		return result;
	}
}