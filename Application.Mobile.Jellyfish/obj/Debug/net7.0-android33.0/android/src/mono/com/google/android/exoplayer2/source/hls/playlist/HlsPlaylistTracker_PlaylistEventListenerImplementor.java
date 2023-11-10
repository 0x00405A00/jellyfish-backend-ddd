package mono.com.google.android.exoplayer2.source.hls.playlist;


public class HlsPlaylistTracker_PlaylistEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.source.hls.playlist.HlsPlaylistTracker.PlaylistEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPlaylistChanged:()V:GetOnPlaylistChangedHandler:Com.Google.Android.Exoplayer2.Source.Hls.Playlist.IHlsPlaylistTracker/IPlaylistEventListenerInvoker, ExoPlayer.Hls\n" +
			"n_onPlaylistError:(Landroid/net/Uri;Lcom/google/android/exoplayer2/upstream/LoadErrorHandlingPolicy$LoadErrorInfo;Z)Z:GetOnPlaylistError_Landroid_net_Uri_Lcom_google_android_exoplayer2_upstream_LoadErrorHandlingPolicy_LoadErrorInfo_ZHandler:Com.Google.Android.Exoplayer2.Source.Hls.Playlist.IHlsPlaylistTracker/IPlaylistEventListenerInvoker, ExoPlayer.Hls\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Source.Hls.Playlist.IHlsPlaylistTracker+IPlaylistEventListenerImplementor, ExoPlayer.Hls", HlsPlaylistTracker_PlaylistEventListenerImplementor.class, __md_methods);
	}


	public HlsPlaylistTracker_PlaylistEventListenerImplementor ()
	{
		super ();
		if (getClass () == HlsPlaylistTracker_PlaylistEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Source.Hls.Playlist.IHlsPlaylistTracker+IPlaylistEventListenerImplementor, ExoPlayer.Hls", "", this, new java.lang.Object[] {  });
		}
	}


	public void onPlaylistChanged ()
	{
		n_onPlaylistChanged ();
	}

	private native void n_onPlaylistChanged ();


	public boolean onPlaylistError (android.net.Uri p0, com.google.android.exoplayer2.upstream.LoadErrorHandlingPolicy.LoadErrorInfo p1, boolean p2)
	{
		return n_onPlaylistError (p0, p1, p2);
	}

	private native boolean n_onPlaylistError (android.net.Uri p0, com.google.android.exoplayer2.upstream.LoadErrorHandlingPolicy.LoadErrorInfo p1, boolean p2);

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
