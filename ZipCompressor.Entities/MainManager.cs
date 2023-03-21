using ZipCompressor.Entities;
using ZipCompressor.Entities.ZipCompressor;

namespace ZipCompressor.Entities
{
	public class MainManager
	{
		private static readonly MainManager _instance = new MainManager();

		private MainManager()
		{
			Init();
		}

		private void Init()
		{
			CompressManager = new CompressManager();
			// Init all managers here with the log manager in the constructor
		}



		public static MainManager Instance
		{
			get
			{
				return _instance;
			}
		}


		public CompressManager CompressManager;

	}
}



