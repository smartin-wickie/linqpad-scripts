<Query Kind="Program" />

/*
Interesting scenario where a report was outputing the INT values as HEX
This replaces the Hex values with the INT representation
*/

///Convert Inline Hex to Int
void Main()
{
	var sb = new StringBuilder();
	var innerSb = new StringBuilder();
	int charOffset = 0;
	
	foreach (var line in System.IO.File.ReadAllLines(@"C:\Users\smartin\Desktop\ScratchLog.txt"))
	{
		var hexCodeMatches = Regex.Matches(line, "0x[0-9ABCDEF]+");
		
		if (hexCodeMatches.Count > 0)
		{
			innerSb.Length = 0;
			charOffset = 0;
			for (int i = 0; i < hexCodeMatches.Count; i++)
			{
				var current = hexCodeMatches[i];
				var sel = line.Substring(charOffset,(current.Index - charOffset));
				innerSb.Append(sel);
				innerSb.Append(Convert.ToUInt32(current.Value,16)); //Convert Hex to Int
				charOffset = current.Index + current.Length;

				if (i >= (hexCodeMatches.Count-1))
				{
					var fin = line.Substring(charOffset);
					innerSb.Append(fin);
				}
			}
			sb.AppendLine(innerSb.ToString());
		}
		else
		{
			sb.AppendLine(line);
		}
	}
	
		sb.ToString().Dump();
	
}



/*EXAMPLE FILE
01/05/2020 04:24:17 PM [Error] Unknown Error 0x1A Rows Affected 0x32145 
01/01/2020 07:34:01 AM [Error] Unknown Error 0x1A Rows Affected 0x32145 
01/03/2020 05:39:17 AM [Error] Unknown Error 0x2A Rows Affected 0x321A5 
01/06/2020 12:59:11 AM [Error] Unknown Error 0x1A Rows Affected 0x3B145 
01/04/2020 09:27:36 AM [Error] Unknown Error 0x13 Rows Affected 0x32145 
01/06/2020 08:01:26 PM [Error] Unknown Error 0x1B Rows Affected 0x42145 
01/02/2020 04:39:38 PM [Error] Unknown Error 0x1A Rows Affected 0x31145 
01/05/2020 06:34:22 PM [Error] Unknown Error 0x1A Rows Affected 0x12C4A 
*/
