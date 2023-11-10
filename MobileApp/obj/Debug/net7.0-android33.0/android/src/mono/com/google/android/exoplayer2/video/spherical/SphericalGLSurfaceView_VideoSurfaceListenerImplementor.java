package mono.com.google.android.exoplayer2.video.spherical;


public class SphericalGLSurfaceView_VideoSurfaceListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.video.spherical.SphericalGLSurfaceView.VideoSurfaceListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVideoSurfaceCreated:(Landroid/view/Surface;)V:GetOnVideoSurfaceCreated_Landroid_view_Surface_Handler:Com.Google.Android.Exoplayer2.Video.Spherical.SphericalGLSurfaceView/IVideoSurfaceListenerInvoker, ExoPlayer.Core\n" +
			"n_onVideoSurfaceDestroyed:(Landroid/view/Surface;)V:GetOnVideoSurfaceDestroyed_Landroid_view_Surface_Handler:Com.Google.Android.Exoplayer2.Video.Spherical.SphericalGLSurfaceView/IVideoSurfaceListenerInvoker, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Video.Spherical.SphericalGLSurfaceView+IVideoSurfaceListenerImplementor, ExoPlayer.Core", SphericalGLSurfaceView_VideoSurfaceListenerImplementor.class, __md_methods);
	}


	public SphericalGLSurfaceView_VideoSurfaceListenerImplementor ()
	{
		super ();
		if (getClass () == SphericalGLSurfaceView_VideoSurfaceListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Video.Spherical.SphericalGLSurfaceView+IVideoSurfaceListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onVideoSurfaceCreated (android.view.Surface p0)
	{
		n_onVideoSurfaceCreated (p0);
	}

	private native void n_onVideoSurfaceCreated (android.view.Surface p0);


	public void onVideoSurfaceDestroyed (android.view.Surface p0)
	{
		n_onVideoSurfaceDestroyed (p0);
	}

	private native void n_onVideoSurfaceDestroyed (android.view.Surface p0);

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
