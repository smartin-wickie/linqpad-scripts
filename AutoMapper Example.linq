<Query Kind="Program">
  <Connection>
    <ID>45a17493-a2d7-4707-bdd5-3f99883f6519</ID>
    <Server>SPARKY</Server>
    <Database>AdventureWorks</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference Version="4.0.4">AutoMapper</NuGetReference>
</Query>

void Main()
{
	/*
	Uses Adventure Works DB
	In-Linqpad connection
	Just a simple illustration of setting up an automap		
	*/
	
	AutoMapper.Mapper.Reset();
	DoMapps();
	
	LINQPad.User.Person y = Persons.Where(w => w.BusinessEntityID ==291).Take(1).First();
	PersonDTO z;
	
	z = AutoMapper.Mapper.Map<LINQPad.User.Person, PersonDTO>(y);
	z.Dump();
}

void DebugMap(object s, object d)
{
	(new {src=s.GetType().Name, dest=d.GetType().Name }).Dump();
}


private void DoMapps()
{
	AutoMapper.Mapper.CreateMap<LINQPad.User.Person, PersonDTO>()
		.BeforeMap((s,d) =>DebugMap(s,d) );
	AutoMapper.Mapper.CreateMap<LINQPad.User.BusinessEntity, BusinessEntityDTO>()
		.BeforeMap((s,d) =>DebugMap(s,d) );	
	AutoMapper.Mapper.CreateMap<LINQPad.User.BusinessEntityContact, BusinessEntityContactDTO>()
		.BeforeMap((s,d) =>DebugMap(s,d) );	
	
}

public class PersonDTO
{
	public Guid RowGuid;
	public string LastName;
	public string FirstName;
	public string Title;
	public string Suffix;
	public BusinessEntityDTO BusinessEntity;
	public IList<BusinessEntityContactDTO> BusinessEntityContacts;
}

public class BusinessEntityDTO
{
	public Guid RowGuid;
	public PersonDTO Person;
	public DateTime ModifiedDate;
	public IList<BusinessEntityContactDTO> BusinessEntityContacts;
}

public class BusinessEntityContactDTO
{
	public Guid RowGuid;
	public BusinessEntityDTO BusinessEntity;
	public PersonDTO Person;
}

