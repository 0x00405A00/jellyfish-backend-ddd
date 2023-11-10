package mono.com.google.android.exoplayer2.ui;


public class StyledPlayerView_FullscreenButtonClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.ui.StyledPlayerView.FullscreenButtonClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFullscreenButtonClick:(Z)V:GetOnFullscreenButtonClick_ZHandler:Com.Google.Android.Exoplayer2.UI.StyledPlayerView/IFullscreenButtonClickListenerInvoker, ExoPlayer.UI\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.UI.StyledPlayerView+IFullscreenButtonClickListenerImplementor, ExoPlayer.UI", StyledPlayerView_FullscreenButtonClickListenerImplementor.class, __md_methods);
	}


	public StyledPlayerView_FullscreenButtonClickListenerImplementor ()
	{
		super ();
		if (getClass () == StyledPlayerView_FullscreenButtonClickListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.UI.StyledPlayerView+IFullscreenButtonClickListenerImplementor, ExoPlayer.UI", "", this, new java.lang.Object[] {  });
		}
	}


	public void onFullscreenButtonClick (boolean p0)
	{
		n_onFullscreenButtonClick (p0);
	}

	private native void n_onFullscreenButtonClick (boolean p0);

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
