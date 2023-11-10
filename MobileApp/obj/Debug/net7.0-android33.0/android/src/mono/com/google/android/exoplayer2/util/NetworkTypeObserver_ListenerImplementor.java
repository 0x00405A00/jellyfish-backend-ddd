package mono.com.google.android.exoplayer2.util;


public class NetworkTypeObserver_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.util.NetworkTypeObserver.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNetworkTypeChanged:(I)V:GetOnNetworkTypeChanged_IHandler:Com.Google.Android.Exoplayer2.Util.NetworkTypeObserver/IListenerInvoker, ExoPlayer.Common\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Util.NetworkTypeObserver+IListenerImplementor, ExoPlayer.Common", NetworkTypeObserver_ListenerImplementor.class, __md_methods);
	}


	public NetworkTypeObserver_ListenerImplementor ()
	{
		super ();
		if (getClass () == NetworkTypeObserver_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Util.NetworkTypeObserver+IListenerImplementor, ExoPlayer.Common", "", this, new java.lang.Object[] {  });
		}
	}


	public void onNetworkTypeChanged (int p0)
	{
		n_onNetworkTypeChanged (p0);
	}

	private native void n_onNetworkTypeChanged (int p0);

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
