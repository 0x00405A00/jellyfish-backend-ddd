package mono.com.google.android.exoplayer2.drm;


public class DrmSessionEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.drm.DrmSessionEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDrmKeysLoaded:(ILcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;)V:GetOnDrmKeysLoaded_ILcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_Handler:Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListener, ExoPlayer.Core\n" +
			"n_onDrmKeysRemoved:(ILcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;)V:GetOnDrmKeysRemoved_ILcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_Handler:Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListener, ExoPlayer.Core\n" +
			"n_onDrmKeysRestored:(ILcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;)V:GetOnDrmKeysRestored_ILcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_Handler:Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListener, ExoPlayer.Core\n" +
			"n_onDrmSessionAcquired:(ILcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;I)V:GetOnDrmSessionAcquired_ILcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_IHandler:Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListener, ExoPlayer.Core\n" +
			"n_onDrmSessionManagerError:(ILcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;Ljava/lang/Exception;)V:GetOnDrmSessionManagerError_ILcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListener, ExoPlayer.Core\n" +
			"n_onDrmSessionReleased:(ILcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;)V:GetOnDrmSessionReleased_ILcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_Handler:Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListener, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListenerImplementor, ExoPlayer.Core", DrmSessionEventListenerImplementor.class, __md_methods);
	}


	public DrmSessionEventListenerImplementor ()
	{
		super ();
		if (getClass () == DrmSessionEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Drm.IDrmSessionEventListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onDrmKeysLoaded (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmKeysLoaded (p0, p1);
	}

	private native void n_onDrmKeysLoaded (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1);


	public void onDrmKeysRemoved (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmKeysRemoved (p0, p1);
	}

	private native void n_onDrmKeysRemoved (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1);


	public void onDrmKeysRestored (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmKeysRestored (p0, p1);
	}

	private native void n_onDrmKeysRestored (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1);


	public void onDrmSessionAcquired (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1, int p2)
	{
		n_onDrmSessionAcquired (p0, p1, p2);
	}

	private native void n_onDrmSessionAcquired (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1, int p2);


	public void onDrmSessionManagerError (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1, java.lang.Exception p2)
	{
		n_onDrmSessionManagerError (p0, p1, p2);
	}

	private native void n_onDrmSessionManagerError (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1, java.lang.Exception p2);


	public void onDrmSessionReleased (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmSessionReleased (p0, p1);
	}

	private native void n_onDrmSessionReleased (int p0, com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p1);

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
