package mono.com.google.android.exoplayer2;


public class ExoPlayer_AudioOffloadListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.ExoPlayer.AudioOffloadListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onExperimentalOffloadSchedulingEnabledChanged:(Z)V:GetOnExperimentalOffloadSchedulingEnabledChanged_ZHandler:Com.Google.Android.Exoplayer2.IExoPlayer/IAudioOffloadListener, ExoPlayer.Core\n" +
			"n_onExperimentalOffloadedPlayback:(Z)V:GetOnExperimentalOffloadedPlayback_ZHandler:Com.Google.Android.Exoplayer2.IExoPlayer/IAudioOffloadListener, ExoPlayer.Core\n" +
			"n_onExperimentalSleepingForOffloadChanged:(Z)V:GetOnExperimentalSleepingForOffloadChanged_ZHandler:Com.Google.Android.Exoplayer2.IExoPlayer/IAudioOffloadListener, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.IExoPlayer+IAudioOffloadListenerImplementor, ExoPlayer.Core", ExoPlayer_AudioOffloadListenerImplementor.class, __md_methods);
	}


	public ExoPlayer_AudioOffloadListenerImplementor ()
	{
		super ();
		if (getClass () == ExoPlayer_AudioOffloadListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.IExoPlayer+IAudioOffloadListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onExperimentalOffloadSchedulingEnabledChanged (boolean p0)
	{
		n_onExperimentalOffloadSchedulingEnabledChanged (p0);
	}

	private native void n_onExperimentalOffloadSchedulingEnabledChanged (boolean p0);


	public void onExperimentalOffloadedPlayback (boolean p0)
	{
		n_onExperimentalOffloadedPlayback (p0);
	}

	private native void n_onExperimentalOffloadedPlayback (boolean p0);


	public void onExperimentalSleepingForOffloadChanged (boolean p0)
	{
		n_onExperimentalSleepingForOffloadChanged (p0);
	}

	private native void n_onExperimentalSleepingForOffloadChanged (boolean p0);

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
