package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("Application.Mobile.Jellyfish.MainApplication, Application.Mobile.Jellyfish, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc64a91c395310afd2db.MainApplication.class, crc64a91c395310afd2db.MainApplication.__md_methods);
		mono.android.Runtime.register ("Microsoft.Maui.MauiApplication, Microsoft.Maui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc6488302ad6e9e4df1a.MauiApplication.class, crc6488302ad6e9e4df1a.MauiApplication.__md_methods);
		
	}
}
