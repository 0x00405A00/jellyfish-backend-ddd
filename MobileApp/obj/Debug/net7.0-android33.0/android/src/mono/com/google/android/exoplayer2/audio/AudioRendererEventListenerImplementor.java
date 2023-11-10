package mono.com.google.android.exoplayer2.audio;


public class AudioRendererEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.audio.AudioRendererEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioCodecError:(Ljava/lang/Exception;)V:GetOnAudioCodecError_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioDecoderInitialized:(Ljava/lang/String;JJ)V:GetOnAudioDecoderInitialized_Ljava_lang_String_JJHandler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioDecoderReleased:(Ljava/lang/String;)V:GetOnAudioDecoderReleased_Ljava_lang_String_Handler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioDisabled:(Lcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnAudioDisabled_Lcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioEnabled:(Lcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnAudioEnabled_Lcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioInputFormatChanged:(Lcom/google/android/exoplayer2/Format;Lcom/google/android/exoplayer2/decoder/DecoderReuseEvaluation;)V:GetOnAudioInputFormatChanged_Lcom_google_android_exoplayer2_Format_Lcom_google_android_exoplayer2_decoder_DecoderReuseEvaluation_Handler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioPositionAdvancing:(J)V:GetOnAudioPositionAdvancing_JHandler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioSinkError:(Ljava/lang/Exception;)V:GetOnAudioSinkError_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onAudioUnderrun:(IJJ)V:GetOnAudioUnderrun_IJJHandler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"n_onSkipSilenceEnabledChanged:(Z)V:GetOnSkipSilenceEnabledChanged_ZHandler:Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListener, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListenerImplementor, ExoPlayer.Core", AudioRendererEventListenerImplementor.class, __md_methods);
	}


	public AudioRendererEventListenerImplementor ()
	{
		super ();
		if (getClass () == AudioRendererEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Audio.IAudioRendererEventListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onAudioCodecError (java.lang.Exception p0)
	{
		n_onAudioCodecError (p0);
	}

	private native void n_onAudioCodecError (java.lang.Exception p0);


	public void onAudioDecoderInitialized (java.lang.String p0, long p1, long p2)
	{
		n_onAudioDecoderInitialized (p0, p1, p2);
	}

	private native void n_onAudioDecoderInitialized (java.lang.String p0, long p1, long p2);


	public void onAudioDecoderReleased (java.lang.String p0)
	{
		n_onAudioDecoderReleased (p0);
	}

	private native void n_onAudioDecoderReleased (java.lang.String p0);


	public void onAudioDisabled (com.google.android.exoplayer2.decoder.DecoderCounters p0)
	{
		n_onAudioDisabled (p0);
	}

	private native void n_onAudioDisabled (com.google.android.exoplayer2.decoder.DecoderCounters p0);


	public void onAudioEnabled (com.google.android.exoplayer2.decoder.DecoderCounters p0)
	{
		n_onAudioEnabled (p0);
	}

	private native void n_onAudioEnabled (com.google.android.exoplayer2.decoder.DecoderCounters p0);


	public void onAudioInputFormatChanged (com.google.android.exoplayer2.Format p0, com.google.android.exoplayer2.decoder.DecoderReuseEvaluation p1)
	{
		n_onAudioInputFormatChanged (p0, p1);
	}

	private native void n_onAudioInputFormatChanged (com.google.android.exoplayer2.Format p0, com.google.android.exoplayer2.decoder.DecoderReuseEvaluation p1);


	public void onAudioPositionAdvancing (long p0)
	{
		n_onAudioPositionAdvancing (p0);
	}

	private native void n_onAudioPositionAdvancing (long p0);


	public void onAudioSinkError (java.lang.Exception p0)
	{
		n_onAudioSinkError (p0);
	}

	private native void n_onAudioSinkError (java.lang.Exception p0);


	public void onAudioUnderrun (int p0, long p1, long p2)
	{
		n_onAudioUnderrun (p0, p1, p2);
	}

	private native void n_onAudioUnderrun (int p0, long p1, long p2);


	public void onSkipSilenceEnabledChanged (boolean p0)
	{
		n_onSkipSilenceEnabledChanged (p0);
	}

	private native void n_onSkipSilenceEnabledChanged (boolean p0);

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
