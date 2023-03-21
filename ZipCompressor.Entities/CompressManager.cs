using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace ZipCompressor.Entities
{
	using System;
	using System.IO;
	using ICSharpCode.SharpZipLib.Zip;

	namespace ZipCompressor
	{
		public class CompressManager
		{



			public string CreateZipFile(string path)
			{
				// Create a new zip file
				// Get the full path input from the UI
				string fullPath = path;
				string zipFileName = Path.GetFileNameWithoutExtension(fullPath) + ".zip";
				string zipFileOutputPath = Path.GetDirectoryName(fullPath) + "\\" + zipFileName;

				using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(zipFileOutputPath)))
				{

					// Set the compression level ( 9 = maximum compression)
					zipOutputStream.SetLevel(9);

					// Recursively add all files and directories in the specified path to the zip file
					AddFolderToZip(path, zipOutputStream);

					// Close the zip file
					zipOutputStream.Finish();
					zipOutputStream.Close();
				}
				return File.Exists(zipFileOutputPath) ? zipFileOutputPath : null;

			}

			private static void AddFolderToZip(string path, ZipOutputStream zipOutputStream)
			{
				// Get all files and directories in the specified path
				string[] files = Directory.GetFiles(path);
				string[] directories = Directory.GetDirectories(path);

				// Add each file to the zip file
				foreach (string file in files)
				{
					// Create a new entry in the zip file
					ZipEntry zipEntry = new ZipEntry(Path.GetFileName(file)); // ZipEntry is a class that represents a file in the zip file
					zipEntry.DateTime = DateTime.Now;

					// Add the entry to the zip file
					zipOutputStream.PutNextEntry(zipEntry);

					// Add the file contents to the zip file
					using (FileStream fileStream = File.OpenRead(file))
					{
						byte[] buffer = new byte[4096]; // 4KB buffer - avoid loading the entire file into memory at once, which could be inefficient and slow for large files.
						int bytesRead;
						do
						{
							bytesRead = fileStream.Read(buffer, 0, buffer.Length); // Extract the next chunk of data from the file
							zipOutputStream.Write(buffer, 0, bytesRead); // Add the chunk to the zip file
						} while (bytesRead > 0);
					}

					// Complete the current entry
					zipOutputStream.CloseEntry();
				}

				// Recursively add each subdirectory to the zip file
				foreach (string directory in directories)
				{
					AddFolderToZip(directory, zipOutputStream);
				}
			}
		}
	}
}

