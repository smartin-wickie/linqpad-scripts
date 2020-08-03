<Query Kind="Program" />

void Main()
{
	var z = new MySpeshulClassWillNotErr("Blegh", "Yarf");
	z.Dump();
	
	var y = new MyOtherSpeshulErr(500, "");
	y.Dump();
}

/*
Had a scenario where we wanted to have a error/validation message with internal and external messages
The developer wanted to to make sure that all future implementations followed this convention
*/

// Here is a base abstract with an internal constructor
// Reminder protected access is visible to those that inherit from the base and not outside.
abstract public class BaseClass
{
	protected string _msg1;
	protected string _msg2;
	protected BaseClass(string msg1, string msg2)
	{
		_msg1 = msg1;
		_msg2 = msg2;

	}

	public string MSGA
	{
		get { return _msg1; }

	}

	public string MSGB
	{
		get { return _msg2; }

	}
}

//This class inherits and honors the base constructor.
//It is even beholden to the base construction when it overloads its own constructor.
public class MySpeshulClassWillNotErr : BaseClass
{
	public MySpeshulClassWillNotErr(string msg1, string msg2) : base(msg1, msg2) {}
	public MySpeshulClassWillNotErr(int Val1, string msg2) : base(Val1.ToString(), msg2) { }
}

//This errors at compile for not having a constructor and inheriting
public class NotConstructorWillError : BaseClass
{

}

//This errors at compile for not honoring the base constructor signature while it inherits
public class MyOtherSpeshulErr : BaseClass
{
	public WrondSignatureWillError(int Val1, string msg2)
	{
	}
}