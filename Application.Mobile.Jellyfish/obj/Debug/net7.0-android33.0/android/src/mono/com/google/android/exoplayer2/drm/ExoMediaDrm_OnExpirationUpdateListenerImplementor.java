package mono.com.google.android.exoplayer2.drm;


public class ExoMediaDrm_OnExpirationUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.drm.ExoMediaDrm.OnExpirationUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onExpirationUpdate:(Lcom/google/android/exoplayer2/drm/ExoMediaDrm;[BJ)V:GetOnExpirationUpdate_Lcom_google_android_exoplayer2_drm_ExoMediaDrm_arrayBJHandler:Com.Google.Android.Exoplayer2.Drm.IExoMediaDrm/IOnExpirationUpdateListenerInvoker, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Drm.IExoMediaDrm+IOnExpirationUpdateListenerImplementor, ExoPlayer.Core", ExoMediaDrm_OnExpirationUpdateListenerImplementor.class, __md_methods);
	}


	public ExoMediaDrm_OnExpirationUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == ExoMediaDrm_OnExpirationUpdateListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Drm.IExoMediaDrm+IOnExpirationUpdateListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onExpirationUpdate (com.google.android.exoplayer2.drm.ExoMediaDrm p0, byte[] p1, long p2)
	{
		n_onExpirationUpdate (p0, p1, p2);
	}

	private native void n_onExpirationUpdate (com.google.android.exoplayer2.drm.ExoMediaDrm p0, byte[] p1, long p2);

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
