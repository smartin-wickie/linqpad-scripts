<Query Kind="Program" />

/*
Generate large binary file
Pseudo Random 
Disk space is cheap
*/
void Main()
{
	MemoryStream m = new MemoryStream();
	BinaryWriter b = new BinaryWriter(m);
	Random r;
	
	
	for (int i = 0; i < 1000; i++)
	{
		r = new Random(i);
		for (int k = 0; k < 1000; k++)
		{
			b.Write(r.Next());
		}
	}
	
	System.IO.File.WriteAllBytes(@"C:\temp\lessrandomshite.bin",m.ToArray());
}