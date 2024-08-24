using Xunit;
using System;
using System.IO;
using System.Text;
using DotNetKoans.Engine;
using IOPath = System.IO.Path;

namespace DotNetKoans.Koans;

public class AboutFile : Koan
{
	// File is a class that provides static methods for creating, copying, deleting, moving,
	// and opening files, and helps in the creation of FileStream objects.

	[Step(1)]
	public void CreatingAndDeletingFile()
	{
		string path = IOPath.GetTempFileName(); // GetTempFileName() Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
		Assert.True(File.Exists(path)); 
		File.Delete(path);
		Assert.False(File.Exists(path));
	}
        
	[Step(2)]
	public void CopyFile()
	{
		string path = IOPath.GetTempFileName();
		string newPath = IOPath.Combine(IOPath.GetTempPath(), "newFile.txt");

		File.Delete(newPath);
		File.Copy(path, newPath);

		Assert.True(File.Exists(path));
		Assert.True(File.Exists(newPath));
	}

	[Step(3)]
	public void MoveFile()
	{
		string path = IOPath.GetTempFileName();
		//Console.WriteLine(path);
		string newPath = IOPath.Combine(IOPath.GetTempPath(), "newFile.txt");
		/*Console.WriteLine(newPath);
		Console.ReadLine();*/

		if(File.Exists(newPath))   
			File.Delete(newPath);
		File.Move(path, newPath);
		/*Console.WriteLine(path);
		Console.WriteLine(newPath);
		Console.ReadLine();*/

		Assert.False(File.Exists(path));
		Assert.True(File.Exists(newPath));
	}
        
	[Step(4)]
	public void GetFileInfo()
	{
		//string path = IOPath.GetTempFileName();
		string newPath = IOPath.Combine(IOPath.GetTempPath(), "newFile2.txt");
		FileInfo fileInfo = new FileInfo(newPath);
		Console.WriteLine(newPath); Console.WriteLine(fileInfo.Name); Console.WriteLine(fileInfo.FullName);
		Console.ReadLine();

		//Assert.True(fileInfo.Exists);
		//Assert.Equal("/temp/newFile2.txt", fileInfo.FullName); // Ubuntu24.04
		Assert.Equal(@"C:\Users\ericf\AppData\Local\Temp\newFile2.txt", fileInfo.FullName);
	}
        
	[Step(5)]
	public void ReadFile()
	{
		string data = "Hello World!";
		string path = createFileAndFillIn(data);

		byte[] bytes = new byte[data.Length];
		UTF8Encoding temp = new UTF8Encoding(true);
		string readMessage = "";
		using (FileStream fs = File.OpenRead(path))
		{
			while (fs.Read(bytes, 0, bytes.Length) > 0)
			{
				readMessage = temp.GetString(bytes);
			}
		}
		Assert.Equal("Hello World!", readMessage); // what is the message?
	}

	[Step(6)]
	public void ReadLines()
	{

		string data = "Line0\nLine1\nLine2";
		string path = createFileAndFillIn(data);

		var lines = File.ReadAllLines(path);
            
		Assert.Equal(3, lines.Length); // what is the number of lines?
		Assert.Equal("Line1", lines[1]); // what is written in the line No.2 ?
	}

	private string createFileAndFillIn(string data)
	{
		string path = IOPath.GetTempFileName();
		byte[] info = new UTF8Encoding(true).GetBytes(data);
		using (FileStream fs = File.OpenWrite(path))
		{
			fs.Write(info, 0, info.Length);
		}
		return path;
	}
}