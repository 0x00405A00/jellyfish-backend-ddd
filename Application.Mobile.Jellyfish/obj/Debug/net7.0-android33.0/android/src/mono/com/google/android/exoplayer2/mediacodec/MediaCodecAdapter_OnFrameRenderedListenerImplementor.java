package mono.com.google.android.exoplayer2.mediacodec;


public class MediaCodecAdapter_OnFrameRenderedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.mediacodec.MediaCodecAdapter.OnFrameRenderedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFrameRendered:(Lcom/google/android/exoplayer2/mediacodec/MediaCodecAdapter;JJ)V:GetOnFrameRendered_Lcom_google_android_exoplayer2_mediacodec_MediaCodecAdapter_JJHandler:Com.Google.Android.Exoplayer2.Mediacodec.IMediaCodecAdapter/IOnFrameRenderedListenerInvoker, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Mediacodec.IMediaCodecAdapter+IOnFrameRenderedListenerImplementor, ExoPlayer.Core", MediaCodecAdapter_OnFrameRenderedListenerImplementor.class, __md_methods);
	}


	public MediaCodecAdapter_OnFrameRenderedListenerImplementor ()
	{
		super ();
		if (getClass () == MediaCodecAdapter_OnFrameRenderedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Mediacodec.IMediaCodecAdapter+IOnFrameRenderedListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onFrameRendered (com.google.android.exoplayer2.mediacodec.MediaCodecAdapter p0, long p1, long p2)
	{
		n_onFrameRendered (p0, p1, p2);
	}

	private native void n_onFrameRendered (com.google.android.exoplayer2.mediacodec.MediaCodecAdapter p0, long p1, long p2);

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
