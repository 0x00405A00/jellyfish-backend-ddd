package mono.com.google.android.exoplayer2;


public class Renderer_WakeupListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.Renderer.WakeupListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSleep:()V:GetOnSleepHandler:Com.Google.Android.Exoplayer2.IRenderer/IWakeupListenerInvoker, ExoPlayer.Core\n" +
			"n_onWakeup:()V:GetOnWakeupHandler:Com.Google.Android.Exoplayer2.IRenderer/IWakeupListenerInvoker, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.IRenderer+IWakeupListenerImplementor, ExoPlayer.Core", Renderer_WakeupListenerImplementor.class, __md_methods);
	}


	public Renderer_WakeupListenerImplementor ()
	{
		super ();
		if (getClass () == Renderer_WakeupListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.IRenderer+IWakeupListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onSleep ()
	{
		n_onSleep ();
	}

	private native void n_onSleep ();


	public void onWakeup ()
	{
		n_onWakeup ();
	}

	private native void n_onWakeup ();

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
