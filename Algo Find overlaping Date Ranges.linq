<Query Kind="Program" />

/*
Very basic way to determine if two date ranges have overlaping dates (Days specifically)
Generally date ranges are turned in epoch days from 1900-1-1
Then uses a Intersect of those int ranges
This is not partcularly eficient  especially for large date ranges
It looks more concise than a series of if/then/else statements
*/

void Main()
{
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 1 2015"), DateTime.Parse("Jan 7 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 11 2015")));  //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 1 2015"), DateTime.Parse("Jan 7 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));  //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 7 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));  //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 17 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));  //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 10 2015"), DateTime.Parse("Jan 17 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));  //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 10 2015"), DateTime.Parse("Jan 27 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));  //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 15 2015"), DateTime.Parse("Jan 27 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));  //False
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 15 2015"), DateTime.Parse("Jan 27 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));   //Fasle
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 15 2015"), DateTime.Parse("Jan 27 2015"), DateTime.Parse("Jan 5 2015"), DateTime.Parse("Jan 12 2015")));   //False
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 15 2015"), DateTime.Parse("Jan 27 2015"), DateTime.Parse("Jan 27 2015"), DateTime.Parse("Feb 2 2015")));   //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 15 2015"), DateTime.Parse("Jan 26 2015"), DateTime.Parse("Jan 27 2015"), DateTime.Parse("Feb 2 2015")));   //False
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 15 2015"), DateTime.Parse("Jan 26 2015"), DateTime.Parse("Jan 25 2015"), DateTime.Parse("Jan 27 2015")));   //True
	Console.WriteLine(DateRangesIntersect(DateTime.Parse("Jan 15 2015"), DateTime.Parse("Jan 26 2015"), DateTime.Parse("Jan 20 2015"), DateTime.Parse("Jan 26 2015")));   //True
}

// Two Date Ranges have intersecting Days
public bool DateRangesIntersect(DateTime Start1, DateTime End1, DateTime Start2, DateTime End2)
{
	var Range1 = Enumerable.Range((Start1 - DateTime.MinValue).Days, (End1.AddDays(1) - Start1).Days );  //Assume that End Date is 0:00 in morning so adding 1 day
	var Range2 = Enumerable.Range((Start2 - DateTime.MinValue).Days, (End2.AddDays(1) - Start2).Days );  //Assume that End Date is 0:00 in morning so adding 1 day
	return Range1.Intersect(Range2).Any();
}