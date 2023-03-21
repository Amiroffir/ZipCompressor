using System.Diagnostics;
using ZipCompressor.Entities;

namespace ZipCompressor.UI
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void CompressBtn_Click(object sender, EventArgs e)
		{
			// Get the full path input from the UI and pass it to the compress manager
			string Link = MainManager.Instance.CompressManager.CreateZipFile(textBox1.Text);
			if (Link != null)
			{
				// If the file was created successfully, return a clickable link to the file
				AddLinkLabel(Link);
			}
			else
			{
				MessageBox.Show("The file has not been compressed successfully");
			}

		}
		private void AddLinkLabel(string Link)
		{
			// Create a new LinkLabel control
			LinkLabel linkLabel = new LinkLabel();

			// Set the text and location of the LinkLabel control
			linkLabel.Text = Link;
			linkLabel.Location = new Point(50, 200);
			linkLabel.AutoSize = true;
			// Set the LinkArea property to specify which part of the text is clickable
			linkLabel.LinkArea = new LinkArea(0, 200);

			// Add the LinkLabel control to the form's Controls collection
			Controls.Add(linkLabel);
			
			// Add an event handler for the LinkClicked event
			linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabel_LinkClicked);
			
			
		}


		private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// open folder
			Process.Start("explorer.exe", "/select," + ((LinkLabel)sender).Text);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}
	}
}