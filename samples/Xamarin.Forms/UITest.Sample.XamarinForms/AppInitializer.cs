using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

using System.Reflection;
using System.Diagnostics;

namespace UITest.Sample.XamarinFormsAndroid
{
	public class AppInitializer
	{
		private static IApp app;

		public static IApp StartApp (Platform platform)
		{
			string currentFile = new Uri (Assembly.GetExecutingAssembly ().CodeBase).LocalPath;
			FileInfo fi = new FileInfo (currentFile);
			string dir_solution = fi.Directory.Parent.Parent.Parent.FullName;
			//string dir_solution = @"..\..\";

			string app_file = 
					//"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/Android/com.infinum.hak-1/base.apk"
					//"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/Android/vecernjilistlite.institut.hr-1/base.apk"
					//"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/iOS/HAK/HAK 2.6.6.ipa"
					"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/iOS/VL/Večernji list 2.24.ipa"
					;

			string app_folder = 
					"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/Android/com.infinum.hak-1/"
					//"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/Android/vecernjilistlite.institut.hr-1/"
					//"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/iOS/HAK/"
					//"/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/installation-packages/iOS/VL/"
					;

			switch (TestEnvironment.Platform)
			{

				case TestPlatform.Local:
					FileSystemInfo fsi = 
								new DirectoryInfo(app_folder)
											.GetFileSystemInfos()
											.OrderByDescending(file => file.LastWriteTimeUtc)
											.First
												(
													file => 
														file.Name.EndsWith(".app") 
														|| 
														file.Name.EndsWith(".apk")
												);

					app = fsi.Name.EndsWith(".app")
						? 
							ConfigureApp
								.iOS
								.AppBundle(fsi.FullName)
								.Debug()
								.StartApp() 
								as IApp
						: 
							ConfigureApp
								.Android
								.ApkFile(fsi.FullName)
								.Debug()
								.StartApp()
								as IApp
								;
					break;
				case TestPlatform.TestCloudiOS:
					app = ConfigureApp.iOS.StartApp();
					break;
				case TestPlatform.TestCloudAndroid:
					app = ConfigureApp.Android.StartApp();
					break;
			}




			if (platform == Platform.Android)
			{
				string dir_apk = @"Sample.XamarinForms\Sample.XamarinForms.Droid\bin\Debug\Sample.XamarinForms.Droid.apk";

				string path_apk = Path.Combine (dir_solution, dir_apk);
				string path_apk1 = "/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/samples/Sample.XamarinForms/Sample.XamarinForms.Droid/bin/Debug/Sample.XamarinForms.Droid.apk";

				Debug.WriteLine("path_apk  = ", path_apk);
				Debug.WriteLine("path_apk1 = ", path_apk1);

				if (! File.Exists (path_apk1))
				{
					throw new InvalidOperationException("APK not found: " + path_apk);
				}


				string api_key = "";

				app = 
					ConfigureApp
						.Android
						.ApkFile(path_apk1)
						.Debug()
        				.WaitTimes(new HolisticWare.XamarinUITest.WaitTimes())
        				//.ApiKey(api_key)
						.StartApp();

				return app; 
			}
			else if (platform == Platform.iOS)
			{
				string dir_app = @"Sample.XamarinForms\Sample.XamarinForms.Droid\bin\Debug\Sample.XamarinForms.Droid.apk";

				string path_app = Path.Combine (dir_solution, dir_app);
				string path_app1 = "/Users/moljac/Projects/Samples/Samples.Xamarin.UITest/samples/Sample.XamarinForms/Sample.XamarinForms.iOS/bin/iPhoneSimulator/Debug/SampleXamarinFormsiOS.app";

				Debug.WriteLine("path_app  = ", path_app);
				Debug.WriteLine("path_app1 = ", path_app1);

				if (! File.Exists (path_app1))
				{
					throw new InvalidOperationException("App not found: " + path_app);
				}

				string api_key = "";
				app = 
					ConfigureApp
						.iOS
						.AppBundle(path_app1)
						//.ApiKey("")
						.StartApp();

				return app;
			}
			else
			{
				throw new ArgumentOutOfRangeException("Platform ??");
			}

		}
	}
}

