package mono.com.google.android.exoplayer2.transformer;


public class Transformer_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.transformer.Transformer.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFallbackApplied:(Lcom/google/android/exoplayer2/MediaItem;Lcom/google/android/exoplayer2/transformer/TransformationRequest;Lcom/google/android/exoplayer2/transformer/TransformationRequest;)V:GetOnFallbackApplied_Lcom_google_android_exoplayer2_MediaItem_Lcom_google_android_exoplayer2_transformer_TransformationRequest_Lcom_google_android_exoplayer2_transformer_TransformationRequest_Handler:Com.Google.Android.Exoplayer2.Transformer.Transformer/IListener, ExoPlayer.Transformer\n" +
			"n_onTransformationCompleted:(Lcom/google/android/exoplayer2/MediaItem;Lcom/google/android/exoplayer2/transformer/TransformationResult;)V:GetOnTransformationCompleted_Lcom_google_android_exoplayer2_MediaItem_Lcom_google_android_exoplayer2_transformer_TransformationResult_Handler:Com.Google.Android.Exoplayer2.Transformer.Transformer/IListener, ExoPlayer.Transformer\n" +
			"n_onTransformationError:(Lcom/google/android/exoplayer2/MediaItem;Lcom/google/android/exoplayer2/transformer/TransformationException;)V:GetOnTransformationError_Lcom_google_android_exoplayer2_MediaItem_Lcom_google_android_exoplayer2_transformer_TransformationException_Handler:Com.Google.Android.Exoplayer2.Transformer.Transformer/IListener, ExoPlayer.Transformer\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Transformer.Transformer+IListenerImplementor, ExoPlayer.Transformer", Transformer_ListenerImplementor.class, __md_methods);
	}


	public Transformer_ListenerImplementor ()
	{
		super ();
		if (getClass () == Transformer_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Transformer.Transformer+IListenerImplementor, ExoPlayer.Transformer", "", this, new java.lang.Object[] {  });
		}
	}


	public void onFallbackApplied (com.google.android.exoplayer2.MediaItem p0, com.google.android.exoplayer2.transformer.TransformationRequest p1, com.google.android.exoplayer2.transformer.TransformationRequest p2)
	{
		n_onFallbackApplied (p0, p1, p2);
	}

	private native void n_onFallbackApplied (com.google.android.exoplayer2.MediaItem p0, com.google.android.exoplayer2.transformer.TransformationRequest p1, com.google.android.exoplayer2.transformer.TransformationRequest p2);


	public void onTransformationCompleted (com.google.android.exoplayer2.MediaItem p0, com.google.android.exoplayer2.transformer.TransformationResult p1)
	{
		n_onTransformationCompleted (p0, p1);
	}

	private native void n_onTransformationCompleted (com.google.android.exoplayer2.MediaItem p0, com.google.android.exoplayer2.transformer.TransformationResult p1);


	public void onTransformationError (com.google.android.exoplayer2.MediaItem p0, com.google.android.exoplayer2.transformer.TransformationException p1)
	{
		n_onTransformationError (p0, p1);
	}

	private native void n_onTransformationError (com.google.android.exoplayer2.MediaItem p0, com.google.android.exoplayer2.transformer.TransformationException p1);

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
