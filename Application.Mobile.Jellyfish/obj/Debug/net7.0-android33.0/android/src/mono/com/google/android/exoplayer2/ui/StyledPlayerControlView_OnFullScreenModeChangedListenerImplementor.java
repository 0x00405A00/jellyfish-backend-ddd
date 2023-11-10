package mono.com.google.android.exoplayer2.ui;


public class StyledPlayerControlView_OnFullScreenModeChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.ui.StyledPlayerControlView.OnFullScreenModeChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFullScreenModeChanged:(Z)V:GetOnFullScreenModeChanged_ZHandler:Com.Google.Android.Exoplayer2.UI.StyledPlayerControlView/IOnFullScreenModeChangedListenerInvoker, ExoPlayer.UI\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.UI.StyledPlayerControlView+IOnFullScreenModeChangedListenerImplementor, ExoPlayer.UI", StyledPlayerControlView_OnFullScreenModeChangedListenerImplementor.class, __md_methods);
	}


	public StyledPlayerControlView_OnFullScreenModeChangedListenerImplementor ()
	{
		super ();
		if (getClass () == StyledPlayerControlView_OnFullScreenModeChangedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.UI.StyledPlayerControlView+IOnFullScreenModeChangedListenerImplementor, ExoPlayer.UI", "", this, new java.lang.Object[] {  });
		}
	}


	public void onFullScreenModeChanged (boolean p0)
	{
		n_onFullScreenModeChanged (p0);
	}

	private native void n_onFullScreenModeChanged (boolean p0);

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
