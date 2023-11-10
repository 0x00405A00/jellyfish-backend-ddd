package mono.com.google.android.exoplayer2.ui;


public class StyledPlayerControlView_VisibilityListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.ui.StyledPlayerControlView.VisibilityListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVisibilityChange:(I)V:GetOnVisibilityChange_IHandler:Com.Google.Android.Exoplayer2.UI.StyledPlayerControlView/IVisibilityListenerInvoker, ExoPlayer.UI\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.UI.StyledPlayerControlView+IVisibilityListenerImplementor, ExoPlayer.UI", StyledPlayerControlView_VisibilityListenerImplementor.class, __md_methods);
	}


	public StyledPlayerControlView_VisibilityListenerImplementor ()
	{
		super ();
		if (getClass () == StyledPlayerControlView_VisibilityListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.UI.StyledPlayerControlView+IVisibilityListenerImplementor, ExoPlayer.UI", "", this, new java.lang.Object[] {  });
		}
	}


	public void onVisibilityChange (int p0)
	{
		n_onVisibilityChange (p0);
	}

	private native void n_onVisibilityChange (int p0);

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
