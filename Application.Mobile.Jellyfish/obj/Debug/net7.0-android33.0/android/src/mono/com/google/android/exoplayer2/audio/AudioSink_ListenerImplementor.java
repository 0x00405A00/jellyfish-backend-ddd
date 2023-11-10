package mono.com.google.android.exoplayer2.audio;


public class AudioSink_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.audio.AudioSink.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPositionDiscontinuity:()V:GetOnPositionDiscontinuityHandler:Com.Google.Android.Exoplayer2.Audio.IAudioSink/IListenerInvoker, ExoPlayer.Core\n" +
			"n_onSkipSilenceEnabledChanged:(Z)V:GetOnSkipSilenceEnabledChanged_ZHandler:Com.Google.Android.Exoplayer2.Audio.IAudioSink/IListenerInvoker, ExoPlayer.Core\n" +
			"n_onUnderrun:(IJJ)V:GetOnUnderrun_IJJHandler:Com.Google.Android.Exoplayer2.Audio.IAudioSink/IListenerInvoker, ExoPlayer.Core\n" +
			"n_onAudioSinkError:(Ljava/lang/Exception;)V:GetOnAudioSinkError_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Audio.IAudioSink/IListener, ExoPlayer.Core\n" +
			"n_onOffloadBufferEmptying:()V:GetOnOffloadBufferEmptyingHandler:Com.Google.Android.Exoplayer2.Audio.IAudioSink/IListener, ExoPlayer.Core\n" +
			"n_onOffloadBufferFull:()V:GetOnOffloadBufferFullHandler:Com.Google.Android.Exoplayer2.Audio.IAudioSink/IListener, ExoPlayer.Core\n" +
			"n_onPositionAdvancing:(J)V:GetOnPositionAdvancing_JHandler:Com.Google.Android.Exoplayer2.Audio.IAudioSink/IListener, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Audio.IAudioSink+IListenerImplementor, ExoPlayer.Core", AudioSink_ListenerImplementor.class, __md_methods);
	}


	public AudioSink_ListenerImplementor ()
	{
		super ();
		if (getClass () == AudioSink_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Audio.IAudioSink+IListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onPositionDiscontinuity ()
	{
		n_onPositionDiscontinuity ();
	}

	private native void n_onPositionDiscontinuity ();


	public void onSkipSilenceEnabledChanged (boolean p0)
	{
		n_onSkipSilenceEnabledChanged (p0);
	}

	private native void n_onSkipSilenceEnabledChanged (boolean p0);


	public void onUnderrun (int p0, long p1, long p2)
	{
		n_onUnderrun (p0, p1, p2);
	}

	private native void n_onUnderrun (int p0, long p1, long p2);


	public void onAudioSinkError (java.lang.Exception p0)
	{
		n_onAudioSinkError (p0);
	}

	private native void n_onAudioSinkError (java.lang.Exception p0);


	public void onOffloadBufferEmptying ()
	{
		n_onOffloadBufferEmptying ();
	}

	private native void n_onOffloadBufferEmptying ();


	public void onOffloadBufferFull ()
	{
		n_onOffloadBufferFull ();
	}

	private native void n_onOffloadBufferFull ();


	public void onPositionAdvancing (long p0)
	{
		n_onPositionAdvancing (p0);
	}

	private native void n_onPositionAdvancing (long p0);

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
