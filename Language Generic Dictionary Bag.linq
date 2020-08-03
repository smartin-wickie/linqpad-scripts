<Query Kind="Program" />

/*
This is abhorent
It is a typed bag with an underlying dictionary keyed by type
I wrote it as a thought experiment to address a need to persist state in a variety of pielines without needing to know what the pielines produce or store
If you wanted to use something like this in production...
	you may be trying to solve the wrong problem or need to revisit your pattern
*/
void Main()
{
	//Initialize "The Bag"
	var typedObjectBag = new TypedObjectBag();
	
	//State components
	var widget = new Widget(){Id = 9001};
	typedObjectBag.Add(widget);

	//More State Components
	var thing = new Thing(){Id=123456, ThingId=987654321};
	typedObjectBag.Add(thing);
	
	//Get the Widget
	var outWidget = typedObjectBag.Get<Widget>();
	outWidget.Dump();

	//Get The Thing
	var outThing = typedObjectBag.Get<Thing>();
	outThing.Dump();
	
	//Cannot get the Base Type
	try 	{	        
		typedObjectBag.Get<ThingBase>().Dump();
	}
	catch (Exception ex) 	{
		ex.Dump();
	}

	//Cannot add a different object if that type already is bagged
	try
	{
		var widget2 = new Widget() { Id = 9002 };
		typedObjectBag.Add(widget2);
	}
	catch (Exception ex) 	{
		ex.Dump();
	}


}

// THE BAG
public class TypedObjectBag
{
	private readonly Dictionary<Type,Object> _dictionary = new Dictionary<System.Type, object>();
	
	public void Add(object addObject)
	{
		var keyType = addObject.GetType();
		if (_dictionary.ContainsKey(keyType))
			throw new Exception($"Object of (Type){keyType.Name} already exists");
		
		_dictionary.Add(addObject.GetType(),addObject);
	}
	
	public T Get<T>()
	{
		var keyType = typeof(T);
		try
		{
			return (T)_dictionary[keyType];
		}
		catch (KeyNotFoundException ex)
		{
			throw new KeyNotFoundException($"The given key (Type){keyType.Name} was not present in the dictionary.", ex);
		}
		catch (Exception)
		{
			throw;
		}
	}
}

//Test Types
public class Widget
{
	public int Id{ get; set;}	
}

public abstract class ThingBase
{
	public int Id{ get; set;}	
}

public class Thing : ThingBase
{
	public int ThingId{ get; set;}	
}