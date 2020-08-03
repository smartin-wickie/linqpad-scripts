<Query Kind="Program" />

void Main()
{
	var x = new FudOfFoo() {FooValue=99, FudValue=77};
	
	//The following is ambiguous and will error at design time
	GetValue(x);
}

public interface IFoo
{
	int FooValue { get; set; } 
}

public interface IFud
{
	int FudValue { get; set; } 
}

public class FudOfFoo : IFoo, IFud
{
	public int FudValue { get; set; } 
	public int FooValue { get; set; } 
}

public void GetValue(IFud fud)
{
	fud.FudValue.Dump("Fud Value");
}

public void GetValue(IFoo foo)
{
	foo.FooValue.Dump("Foo Value");
}