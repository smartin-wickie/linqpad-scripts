<Query Kind="Program" />

void Main()
{
	var unixTimeStamp = 1552420719;
	System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
	dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
	dtDateTime.AddHours(6).ToString("yyyy-MM-dd HH:mm:ss").Dump();
	dtDateTime.AddHours(6).AddSeconds(103).ToString("yyyy-MM-dd HH:mm:ss").Dump();
}

// Define other methods and classes here