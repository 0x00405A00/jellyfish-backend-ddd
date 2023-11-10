package mono.com.google.android.exoplayer2.ui;


public class StyledPlayerView_ControllerVisibilityListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.ui.StyledPlayerView.ControllerVisibilityListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVisibilityChanged:(I)V:GetOnVisibilityChanged_IHandler:Com.Google.Android.Exoplayer2.UI.StyledPlayerView/IControllerVisibilityListenerInvoker, ExoPlayer.UI\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.UI.StyledPlayerView+IControllerVisibilityListenerImplementor, ExoPlayer.UI", StyledPlayerView_ControllerVisibilityListenerImplementor.class, __md_methods);
	}


	public StyledPlayerView_ControllerVisibilityListenerImplementor ()
	{
		super ();
		if (getClass () == StyledPlayerView_ControllerVisibilityListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.UI.StyledPlayerView+IControllerVisibilityListenerImplementor, ExoPlayer.UI", "", this, new java.lang.Object[] {  });
		}
	}


	public void onVisibilityChanged (int p0)
	{
		n_onVisibilityChanged (p0);
	}

	private native void n_onVisibilityChanged (int p0);

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
