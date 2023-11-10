package mono.com.google.android.exoplayer2.source;


public class MaskingMediaPeriod_PrepareListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.source.MaskingMediaPeriod.PrepareListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPrepareComplete:(Lcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;)V:GetOnPrepareComplete_Lcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_Handler:Com.Google.Android.Exoplayer2.Source.MaskingMediaPeriod/IPrepareListenerInvoker, ExoPlayer.Core\n" +
			"n_onPrepareError:(Lcom/google/android/exoplayer2/source/MediaSource$MediaPeriodId;Ljava/io/IOException;)V:GetOnPrepareError_Lcom_google_android_exoplayer2_source_MediaSource_MediaPeriodId_Ljava_io_IOException_Handler:Com.Google.Android.Exoplayer2.Source.MaskingMediaPeriod/IPrepareListenerInvoker, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Source.MaskingMediaPeriod+IPrepareListenerImplementor, ExoPlayer.Core", MaskingMediaPeriod_PrepareListenerImplementor.class, __md_methods);
	}


	public MaskingMediaPeriod_PrepareListenerImplementor ()
	{
		super ();
		if (getClass () == MaskingMediaPeriod_PrepareListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Source.MaskingMediaPeriod+IPrepareListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onPrepareComplete (com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p0)
	{
		n_onPrepareComplete (p0);
	}

	private native void n_onPrepareComplete (com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p0);


	public void onPrepareError (com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p0, java.io.IOException p1)
	{
		n_onPrepareError (p0, p1);
	}

	private native void n_onPrepareError (com.google.android.exoplayer2.source.MediaSource.MediaPeriodId p0, java.io.IOException p1);

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
