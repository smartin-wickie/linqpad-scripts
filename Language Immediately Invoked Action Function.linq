<Query Kind="Program" />

void Main()
{
	/*
	Immediately
	Invoked
	Action
	Expression
	*/
	new Action(() => { Console.WriteLine("Hello World"); })();

	/*
	Not-Quite
	Immediately
	Invoked
	Func
	Expression
	*/
	Func<int,int> addOne = (x) => x+1;
	addOne(1).Dump("Addone");


}


