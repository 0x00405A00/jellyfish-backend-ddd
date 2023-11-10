package mono.com.google.android.exoplayer2.util;


public class FrameProcessor_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.util.FrameProcessor.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFrameProcessingEnded:()V:GetOnFrameProcessingEndedHandler:Com.Google.Android.Exoplayer2.Util.IFrameProcessor/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onFrameProcessingError:(Lcom/google/android/exoplayer2/util/FrameProcessingException;)V:GetOnFrameProcessingError_Lcom_google_android_exoplayer2_util_FrameProcessingException_Handler:Com.Google.Android.Exoplayer2.Util.IFrameProcessor/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onOutputFrameAvailable:(J)V:GetOnOutputFrameAvailable_JHandler:Com.Google.Android.Exoplayer2.Util.IFrameProcessor/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onOutputSizeChanged:(II)V:GetOnOutputSizeChanged_IIHandler:Com.Google.Android.Exoplayer2.Util.IFrameProcessor/IListenerInvoker, ExoPlayer.Common\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Util.IFrameProcessor+IListenerImplementor, ExoPlayer.Common", FrameProcessor_ListenerImplementor.class, __md_methods);
	}


	public FrameProcessor_ListenerImplementor ()
	{
		super ();
		if (getClass () == FrameProcessor_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Util.IFrameProcessor+IListenerImplementor, ExoPlayer.Common", "", this, new java.lang.Object[] {  });
		}
	}


	public void onFrameProcessingEnded ()
	{
		n_onFrameProcessingEnded ();
	}

	private native void n_onFrameProcessingEnded ();


	public void onFrameProcessingError (com.google.android.exoplayer2.util.FrameProcessingException p0)
	{
		n_onFrameProcessingError (p0);
	}

	private native void n_onFrameProcessingError (com.google.android.exoplayer2.util.FrameProcessingException p0);


	public void onOutputFrameAvailable (long p0)
	{
		n_onOutputFrameAvailable (p0);
	}

	private native void n_onOutputFrameAvailable (long p0);


	public void onOutputSizeChanged (int p0, int p1)
	{
		n_onOutputSizeChanged (p0, p1);
	}

	private native void n_onOutputSizeChanged (int p0, int p1);

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
