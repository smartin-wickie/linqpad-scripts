<Query Kind="Program">
  <NuGetReference>Pri.LongPath</NuGetReference>
  <Namespace>Pri.LongPath</Namespace>
</Query>

/*
There are situations where deeply nested file paths can be too long for windows to delete
This Pri library allows for deletion of long file paths.
*/

void Main()
{
	var rootpath = @"D:\BitBucket";
	
	var files = Pri.LongPath.Directory.EnumerateFiles(rootpath, "*.*", SearchOption.AllDirectories);
	foreach (var element in files.OrderByDescending(f => f.Length))
	{
		try
		{
			Pri.LongPath.File.Delete(element);
		}
		catch (Exception ex)
		{
			element.Dump();
			throw;
		}
	}

	var dirs = Pri.LongPath.Directory.EnumerateDirectories(rootpath, "*.*", SearchOption.AllDirectories);
	foreach (var element in dirs.OrderByDescending(f => f.Length))
	{
		try
		{
			Pri.LongPath.Directory.Delete(element);
		}
		catch (Exception ex)
		{
			element.Dump();
			throw;
		}
	}

}