package crc64ceb75e76f4b66147;


public class MediaManager
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.Player.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioAttributesChanged:(Lcom/google/android/exoplayer2/audio/AudioAttributes;)V:GetOnAudioAttributesChanged_Lcom_google_android_exoplayer2_audio_AudioAttributes_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onAudioSessionIdChanged:(I)V:GetOnAudioSessionIdChanged_IHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onAvailableCommandsChanged:(Lcom/google/android/exoplayer2/Player$Commands;)V:GetOnAvailableCommandsChanged_Lcom_google_android_exoplayer2_Player_Commands_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onCues:(Lcom/google/android/exoplayer2/text/CueGroup;)V:GetOnCues_Lcom_google_android_exoplayer2_text_CueGroup_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onDeviceInfoChanged:(Lcom/google/android/exoplayer2/DeviceInfo;)V:GetOnDeviceInfoChanged_Lcom_google_android_exoplayer2_DeviceInfo_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onDeviceVolumeChanged:(IZ)V:GetOnDeviceVolumeChanged_IZHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onEvents:(Lcom/google/android/exoplayer2/Player;Lcom/google/android/exoplayer2/Player$Events;)V:GetOnEvents_Lcom_google_android_exoplayer2_Player_Lcom_google_android_exoplayer2_Player_Events_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onIsLoadingChanged:(Z)V:GetOnIsLoadingChanged_ZHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onIsPlayingChanged:(Z)V:GetOnIsPlayingChanged_ZHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onLoadingChanged:(Z)V:GetOnLoadingChanged_ZHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onMaxSeekToPreviousPositionChanged:(J)V:GetOnMaxSeekToPreviousPositionChanged_JHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onMediaItemTransition:(Lcom/google/android/exoplayer2/MediaItem;I)V:GetOnMediaItemTransition_Lcom_google_android_exoplayer2_MediaItem_IHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onMediaMetadataChanged:(Lcom/google/android/exoplayer2/MediaMetadata;)V:GetOnMediaMetadataChanged_Lcom_google_android_exoplayer2_MediaMetadata_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onMetadata:(Lcom/google/android/exoplayer2/metadata/Metadata;)V:GetOnMetadata_Lcom_google_android_exoplayer2_metadata_Metadata_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlayWhenReadyChanged:(ZI)V:GetOnPlayWhenReadyChanged_ZIHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlaybackParametersChanged:(Lcom/google/android/exoplayer2/PlaybackParameters;)V:GetOnPlaybackParametersChanged_Lcom_google_android_exoplayer2_PlaybackParameters_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlaybackStateChanged:(I)V:GetOnPlaybackStateChanged_IHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlaybackSuppressionReasonChanged:(I)V:GetOnPlaybackSuppressionReasonChanged_IHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlayerError:(Lcom/google/android/exoplayer2/PlaybackException;)V:GetOnPlayerError_Lcom_google_android_exoplayer2_PlaybackException_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlayerErrorChanged:(Lcom/google/android/exoplayer2/PlaybackException;)V:GetOnPlayerErrorChanged_Lcom_google_android_exoplayer2_PlaybackException_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlayerStateChanged:(ZI)V:GetOnPlayerStateChanged_ZIHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPlaylistMetadataChanged:(Lcom/google/android/exoplayer2/MediaMetadata;)V:GetOnPlaylistMetadataChanged_Lcom_google_android_exoplayer2_MediaMetadata_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onPositionDiscontinuity:(I)V:GetOnPositionDiscontinuity_IHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onRenderedFirstFrame:()V:GetOnRenderedFirstFrameHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onRepeatModeChanged:(I)V:GetOnRepeatModeChanged_IHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onSeekBackIncrementChanged:(J)V:GetOnSeekBackIncrementChanged_JHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onSeekForwardIncrementChanged:(J)V:GetOnSeekForwardIncrementChanged_JHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onSeekProcessed:()V:GetOnSeekProcessedHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onShuffleModeEnabledChanged:(Z)V:GetOnShuffleModeEnabledChanged_ZHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onSkipSilenceEnabledChanged:(Z)V:GetOnSkipSilenceEnabledChanged_ZHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onSurfaceSizeChanged:(II)V:GetOnSurfaceSizeChanged_IIHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onTimelineChanged:(Lcom/google/android/exoplayer2/Timeline;I)V:GetOnTimelineChanged_Lcom_google_android_exoplayer2_Timeline_IHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onTrackSelectionParametersChanged:(Lcom/google/android/exoplayer2/trackselection/TrackSelectionParameters;)V:GetOnTrackSelectionParametersChanged_Lcom_google_android_exoplayer2_trackselection_TrackSelectionParameters_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onTracksChanged:(Lcom/google/android/exoplayer2/Tracks;)V:GetOnTracksChanged_Lcom_google_android_exoplayer2_Tracks_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onVideoSizeChanged:(Lcom/google/android/exoplayer2/video/VideoSize;)V:GetOnVideoSizeChanged_Lcom_google_android_exoplayer2_video_VideoSize_Handler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"n_onVolumeChanged:(F)V:GetOnVolumeChanged_FHandler:Com.Google.Android.Exoplayer2.IPlayer/IListenerInvoker, ExoPlayer.Common\n" +
			"";
		mono.android.Runtime.register ("CommunityToolkit.Maui.Core.Views.MediaManager, CommunityToolkit.Maui.MediaElement", MediaManager.class, __md_methods);
	}


	public MediaManager ()
	{
		super ();
		if (getClass () == MediaManager.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Core.Views.MediaManager, CommunityToolkit.Maui.MediaElement", "", this, new java.lang.Object[] {  });
		}
	}


	public void onAudioAttributesChanged (com.google.android.exoplayer2.audio.AudioAttributes p0)
	{
		n_onAudioAttributesChanged (p0);
	}

	private native void n_onAudioAttributesChanged (com.google.android.exoplayer2.audio.AudioAttributes p0);


	public void onAudioSessionIdChanged (int p0)
	{
		n_onAudioSessionIdChanged (p0);
	}

	private native void n_onAudioSessionIdChanged (int p0);


	public void onAvailableCommandsChanged (com.google.android.exoplayer2.Player.Commands p0)
	{
		n_onAvailableCommandsChanged (p0);
	}

	private native void n_onAvailableCommandsChanged (com.google.android.exoplayer2.Player.Commands p0);


	public void onCues (com.google.android.exoplayer2.text.CueGroup p0)
	{
		n_onCues (p0);
	}

	private native void n_onCues (com.google.android.exoplayer2.text.CueGroup p0);


	public void onDeviceInfoChanged (com.google.android.exoplayer2.DeviceInfo p0)
	{
		n_onDeviceInfoChanged (p0);
	}

	private native void n_onDeviceInfoChanged (com.google.android.exoplayer2.DeviceInfo p0);


	public void onDeviceVolumeChanged (int p0, boolean p1)
	{
		n_onDeviceVolumeChanged (p0, p1);
	}

	private native void n_onDeviceVolumeChanged (int p0, boolean p1);


	public void onEvents (com.google.android.exoplayer2.Player p0, com.google.android.exoplayer2.Player.Events p1)
	{
		n_onEvents (p0, p1);
	}

	private native void n_onEvents (com.google.android.exoplayer2.Player p0, com.google.android.exoplayer2.Player.Events p1);


	public void onIsLoadingChanged (boolean p0)
	{
		n_onIsLoadingChanged (p0);
	}

	private native void n_onIsLoadingChanged (boolean p0);


	public void onIsPlayingChanged (boolean p0)
	{
		n_onIsPlayingChanged (p0);
	}

	private native void n_onIsPlayingChanged (boolean p0);


	public void onLoadingChanged (boolean p0)
	{
		n_onLoadingChanged (p0);
	}

	private native void n_onLoadingChanged (boolean p0);


	public void onMaxSeekToPreviousPositionChanged (long p0)
	{
		n_onMaxSeekToPreviousPositionChanged (p0);
	}

	private native void n_onMaxSeekToPreviousPositionChanged (long p0);


	public void onMediaItemTransition (com.google.android.exoplayer2.MediaItem p0, int p1)
	{
		n_onMediaItemTransition (p0, p1);
	}

	private native void n_onMediaItemTransition (com.google.android.exoplayer2.MediaItem p0, int p1);


	public void onMediaMetadataChanged (com.google.android.exoplayer2.MediaMetadata p0)
	{
		n_onMediaMetadataChanged (p0);
	}

	private native void n_onMediaMetadataChanged (com.google.android.exoplayer2.MediaMetadata p0);


	public void onMetadata (com.google.android.exoplayer2.metadata.Metadata p0)
	{
		n_onMetadata (p0);
	}

	private native void n_onMetadata (com.google.android.exoplayer2.metadata.Metadata p0);


	public void onPlayWhenReadyChanged (boolean p0, int p1)
	{
		n_onPlayWhenReadyChanged (p0, p1);
	}

	private native void n_onPlayWhenReadyChanged (boolean p0, int p1);


	public void onPlaybackParametersChanged (com.google.android.exoplayer2.PlaybackParameters p0)
	{
		n_onPlaybackParametersChanged (p0);
	}

	private native void n_onPlaybackParametersChanged (com.google.android.exoplayer2.PlaybackParameters p0);


	public void onPlaybackStateChanged (int p0)
	{
		n_onPlaybackStateChanged (p0);
	}

	private native void n_onPlaybackStateChanged (int p0);


	public void onPlaybackSuppressionReasonChanged (int p0)
	{
		n_onPlaybackSuppressionReasonChanged (p0);
	}

	private native void n_onPlaybackSuppressionReasonChanged (int p0);


	public void onPlayerError (com.google.android.exoplayer2.PlaybackException p0)
	{
		n_onPlayerError (p0);
	}

	private native void n_onPlayerError (com.google.android.exoplayer2.PlaybackException p0);


	public void onPlayerErrorChanged (com.google.android.exoplayer2.PlaybackException p0)
	{
		n_onPlayerErrorChanged (p0);
	}

	private native void n_onPlayerErrorChanged (com.google.android.exoplayer2.PlaybackException p0);


	public void onPlayerStateChanged (boolean p0, int p1)
	{
		n_onPlayerStateChanged (p0, p1);
	}

	private native void n_onPlayerStateChanged (boolean p0, int p1);


	public void onPlaylistMetadataChanged (com.google.android.exoplayer2.MediaMetadata p0)
	{
		n_onPlaylistMetadataChanged (p0);
	}

	private native void n_onPlaylistMetadataChanged (com.google.android.exoplayer2.MediaMetadata p0);


	public void onPositionDiscontinuity (int p0)
	{
		n_onPositionDiscontinuity (p0);
	}

	private native void n_onPositionDiscontinuity (int p0);


	public void onRenderedFirstFrame ()
	{
		n_onRenderedFirstFrame ();
	}

	private native void n_onRenderedFirstFrame ();


	public void onRepeatModeChanged (int p0)
	{
		n_onRepeatModeChanged (p0);
	}

	private native void n_onRepeatModeChanged (int p0);


	public void onSeekBackIncrementChanged (long p0)
	{
		n_onSeekBackIncrementChanged (p0);
	}

	private native void n_onSeekBackIncrementChanged (long p0);


	public void onSeekForwardIncrementChanged (long p0)
	{
		n_onSeekForwardIncrementChanged (p0);
	}

	private native void n_onSeekForwardIncrementChanged (long p0);


	public void onSeekProcessed ()
	{
		n_onSeekProcessed ();
	}

	private native void n_onSeekProcessed ();


	public void onShuffleModeEnabledChanged (boolean p0)
	{
		n_onShuffleModeEnabledChanged (p0);
	}

	private native void n_onShuffleModeEnabledChanged (boolean p0);


	public void onSkipSilenceEnabledChanged (boolean p0)
	{
		n_onSkipSilenceEnabledChanged (p0);
	}

	private native void n_onSkipSilenceEnabledChanged (boolean p0);


	public void onSurfaceSizeChanged (int p0, int p1)
	{
		n_onSurfaceSizeChanged (p0, p1);
	}

	private native void n_onSurfaceSizeChanged (int p0, int p1);


	public void onTimelineChanged (com.google.android.exoplayer2.Timeline p0, int p1)
	{
		n_onTimelineChanged (p0, p1);
	}

	private native void n_onTimelineChanged (com.google.android.exoplayer2.Timeline p0, int p1);


	public void onTrackSelectionParametersChanged (com.google.android.exoplayer2.trackselection.TrackSelectionParameters p0)
	{
		n_onTrackSelectionParametersChanged (p0);
	}

	private native void n_onTrackSelectionParametersChanged (com.google.android.exoplayer2.trackselection.TrackSelectionParameters p0);


	public void onTracksChanged (com.google.android.exoplayer2.Tracks p0)
	{
		n_onTracksChanged (p0);
	}

	private native void n_onTracksChanged (com.google.android.exoplayer2.Tracks p0);


	public void onVideoSizeChanged (com.google.android.exoplayer2.video.VideoSize p0)
	{
		n_onVideoSizeChanged (p0);
	}

	private native void n_onVideoSizeChanged (com.google.android.exoplayer2.video.VideoSize p0);


	public void onVolumeChanged (float p0)
	{
		n_onVolumeChanged (p0);
	}

	private native void n_onVolumeChanged (float p0);

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
