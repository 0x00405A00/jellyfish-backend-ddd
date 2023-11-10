package mono.com.google.android.exoplayer2.analytics;


public class PlaybackSessionManager_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.analytics.PlaybackSessionManager.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAdPlaybackStarted:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;Ljava/lang/String;)V:GetOnAdPlaybackStarted_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Ljava_lang_String_Handler:Com.Google.Android.Exoplayer2.Analytics.IPlaybackSessionManager/IListenerInvoker, ExoPlayer.Core\n" +
			"n_onSessionActive:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnSessionActive_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:Com.Google.Android.Exoplayer2.Analytics.IPlaybackSessionManager/IListenerInvoker, ExoPlayer.Core\n" +
			"n_onSessionCreated:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnSessionCreated_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:Com.Google.Android.Exoplayer2.Analytics.IPlaybackSessionManager/IListenerInvoker, ExoPlayer.Core\n" +
			"n_onSessionFinished:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;Z)V:GetOnSessionFinished_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_ZHandler:Com.Google.Android.Exoplayer2.Analytics.IPlaybackSessionManager/IListenerInvoker, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Analytics.IPlaybackSessionManager+IListenerImplementor, ExoPlayer.Core", PlaybackSessionManager_ListenerImplementor.class, __md_methods);
	}


	public PlaybackSessionManager_ListenerImplementor ()
	{
		super ();
		if (getClass () == PlaybackSessionManager_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Analytics.IPlaybackSessionManager+IListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onAdPlaybackStarted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, java.lang.String p2)
	{
		n_onAdPlaybackStarted (p0, p1, p2);
	}

	private native void n_onAdPlaybackStarted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, java.lang.String p2);


	public void onSessionActive (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onSessionActive (p0, p1);
	}

	private native void n_onSessionActive (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);


	public void onSessionCreated (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onSessionCreated (p0, p1);
	}

	private native void n_onSessionCreated (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);


	public void onSessionFinished (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, boolean p2)
	{
		n_onSessionFinished (p0, p1, p2);
	}

	private native void n_onSessionFinished (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, boolean p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
