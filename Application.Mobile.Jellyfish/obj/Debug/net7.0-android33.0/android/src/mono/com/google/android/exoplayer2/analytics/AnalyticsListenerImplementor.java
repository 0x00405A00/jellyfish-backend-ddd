package mono.com.google.android.exoplayer2.analytics;


public class AnalyticsListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.exoplayer2.analytics.AnalyticsListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioAttributesChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/audio/AudioAttributes;)V:GetOnAudioAttributesChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_audio_AudioAttributes_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioCodecError:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnAudioCodecError_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioDecoderInitialized:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;JJ)V:GetOnAudioDecoderInitialized_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_JJHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioDecoderReleased:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnAudioDecoderReleased_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioDisabled:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnAudioDisabled_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioEnabled:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnAudioEnabled_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioInputFormatChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/Format;Lcom/google/android/exoplayer2/decoder/DecoderReuseEvaluation;)V:GetOnAudioInputFormatChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_Format_Lcom_google_android_exoplayer2_decoder_DecoderReuseEvaluation_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioPositionAdvancing:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;J)V:GetOnAudioPositionAdvancing_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_JHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioSessionIdChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;I)V:GetOnAudioSessionIdChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioSinkError:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnAudioSinkError_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAudioUnderrun:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;IJJ)V:GetOnAudioUnderrun_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IJJHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onAvailableCommandsChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/Player$Commands;)V:GetOnAvailableCommandsChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_Player_Commands_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onBandwidthEstimate:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;IJJ)V:GetOnBandwidthEstimate_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IJJHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onCues:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/text/CueGroup;)V:GetOnCues_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_text_CueGroup_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDecoderDisabled:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;ILcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnDecoderDisabled_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ILcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDecoderEnabled:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;ILcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnDecoderEnabled_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ILcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDecoderInitialized:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;ILjava/lang/String;J)V:GetOnDecoderInitialized_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ILjava_lang_String_JHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDecoderInputFormatChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;ILcom/google/android/exoplayer2/Format;)V:GetOnDecoderInputFormatChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ILcom_google_android_exoplayer2_Format_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDeviceInfoChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/DeviceInfo;)V:GetOnDeviceInfoChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_DeviceInfo_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDeviceVolumeChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;IZ)V:GetOnDeviceVolumeChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IZHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDownstreamFormatChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/source/MediaLoadData;)V:GetOnDownstreamFormatChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_source_MediaLoadData_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDrmKeysLoaded:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;)V:GetOnDrmKeysLoaded_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDrmKeysRemoved:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;)V:GetOnDrmKeysRemoved_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDrmKeysRestored:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;)V:GetOnDrmKeysRestored_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDrmSessionAcquired:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;I)V:GetOnDrmSessionAcquired_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDrmSessionManagerError:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnDrmSessionManagerError_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDrmSessionReleased:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;)V:GetOnDrmSessionReleased_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onDroppedVideoFrames:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;IJ)V:GetOnDroppedVideoFrames_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IJHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onEvents:(Lcom/google/android/exoplayer2/Player;Lcom/google/android/exoplayer2/analytics/AnalyticsListener$Events;)V:GetOnEvents_Lcom_google_android_exoplayer2_Player_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_Events_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onIsLoadingChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Z)V:GetOnIsLoadingChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ZHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onIsPlayingChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Z)V:GetOnIsPlayingChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ZHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onLoadCanceled:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/source/LoadEventInfo;Lcom/google/android/exoplayer2/source/MediaLoadData;)V:GetOnLoadCanceled_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_source_LoadEventInfo_Lcom_google_android_exoplayer2_source_MediaLoadData_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onLoadCompleted:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/source/LoadEventInfo;Lcom/google/android/exoplayer2/source/MediaLoadData;)V:GetOnLoadCompleted_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_source_LoadEventInfo_Lcom_google_android_exoplayer2_source_MediaLoadData_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onLoadError:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/source/LoadEventInfo;Lcom/google/android/exoplayer2/source/MediaLoadData;Ljava/io/IOException;Z)V:GetOnLoadError_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_source_LoadEventInfo_Lcom_google_android_exoplayer2_source_MediaLoadData_Ljava_io_IOException_ZHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onLoadStarted:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/source/LoadEventInfo;Lcom/google/android/exoplayer2/source/MediaLoadData;)V:GetOnLoadStarted_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_source_LoadEventInfo_Lcom_google_android_exoplayer2_source_MediaLoadData_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onLoadingChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Z)V:GetOnLoadingChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ZHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onMaxSeekToPreviousPositionChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;J)V:GetOnMaxSeekToPreviousPositionChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_JHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onMediaItemTransition:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/MediaItem;I)V:GetOnMediaItemTransition_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_MediaItem_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onMediaMetadataChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/MediaMetadata;)V:GetOnMediaMetadataChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_MediaMetadata_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onMetadata:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/metadata/Metadata;)V:GetOnMetadata_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_metadata_Metadata_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlayWhenReadyChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;ZI)V:GetOnPlayWhenReadyChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ZIHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlaybackParametersChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/PlaybackParameters;)V:GetOnPlaybackParametersChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_PlaybackParameters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlaybackStateChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;I)V:GetOnPlaybackStateChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlaybackSuppressionReasonChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;I)V:GetOnPlaybackSuppressionReasonChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlayerError:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/PlaybackException;)V:GetOnPlayerError_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_PlaybackException_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlayerErrorChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/PlaybackException;)V:GetOnPlayerErrorChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_PlaybackException_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlayerReleased:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;)V:GetOnPlayerReleased_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlayerStateChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;ZI)V:GetOnPlayerStateChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ZIHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPlaylistMetadataChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/MediaMetadata;)V:GetOnPlaylistMetadataChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_MediaMetadata_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onPositionDiscontinuity:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/Player$PositionInfo;Lcom/google/android/exoplayer2/Player$PositionInfo;I)V:GetOnPositionDiscontinuity_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_Player_PositionInfo_Lcom_google_android_exoplayer2_Player_PositionInfo_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onRenderedFirstFrame:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/Object;J)V:GetOnRenderedFirstFrame_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_Object_JHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onRepeatModeChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;I)V:GetOnRepeatModeChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onSeekBackIncrementChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;J)V:GetOnSeekBackIncrementChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_JHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onSeekForwardIncrementChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;J)V:GetOnSeekForwardIncrementChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_JHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onSeekProcessed:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;)V:GetOnSeekProcessed_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onSeekStarted:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;)V:GetOnSeekStarted_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onShuffleModeChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Z)V:GetOnShuffleModeChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ZHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onSkipSilenceEnabledChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Z)V:GetOnSkipSilenceEnabledChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_ZHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onSurfaceSizeChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;II)V:GetOnSurfaceSizeChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IIHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onTimelineChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;I)V:GetOnTimelineChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_IHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onTrackSelectionParametersChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/trackselection/TrackSelectionParameters;)V:GetOnTrackSelectionParametersChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_trackselection_TrackSelectionParameters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onTracksChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/Tracks;)V:GetOnTracksChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_Tracks_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onUpstreamDiscarded:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/source/MediaLoadData;)V:GetOnUpstreamDiscarded_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_source_MediaLoadData_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoCodecError:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnVideoCodecError_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoDecoderInitialized:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;JJ)V:GetOnVideoDecoderInitialized_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_JJHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoDecoderReleased:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnVideoDecoderReleased_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoDisabled:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnVideoDisabled_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoEnabled:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/decoder/DecoderCounters;)V:GetOnVideoEnabled_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_decoder_DecoderCounters_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoFrameProcessingOffset:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;JI)V:GetOnVideoFrameProcessingOffset_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_JIHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoInputFormatChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/Format;Lcom/google/android/exoplayer2/decoder/DecoderReuseEvaluation;)V:GetOnVideoInputFormatChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_Format_Lcom_google_android_exoplayer2_decoder_DecoderReuseEvaluation_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVideoSizeChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;Lcom/google/android/exoplayer2/video/VideoSize;)V:GetOnVideoSizeChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_Lcom_google_android_exoplayer2_video_VideoSize_Handler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"n_onVolumeChanged:(Lcom/google/android/exoplayer2/analytics/AnalyticsListener$EventTime;F)V:GetOnVolumeChanged_Lcom_google_android_exoplayer2_analytics_AnalyticsListener_EventTime_FHandler:Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListener, ExoPlayer.Core\n" +
			"";
		mono.android.Runtime.register ("Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListenerImplementor, ExoPlayer.Core", AnalyticsListenerImplementor.class, __md_methods);
	}


	public AnalyticsListenerImplementor ()
	{
		super ();
		if (getClass () == AnalyticsListenerImplementor.class) {
			mono.android.TypeManager.Activate ("Com.Google.Android.Exoplayer2.Analytics.IAnalyticsListenerImplementor, ExoPlayer.Core", "", this, new java.lang.Object[] {  });
		}
	}


	public void onAudioAttributesChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.audio.AudioAttributes p1)
	{
		n_onAudioAttributesChanged (p0, p1);
	}

	private native void n_onAudioAttributesChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.audio.AudioAttributes p1);


	public void onAudioCodecError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onAudioCodecError (p0, p1);
	}

	private native void n_onAudioCodecError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);


	public void onAudioDecoderInitialized (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3)
	{
		n_onAudioDecoderInitialized (p0, p1, p2, p3);
	}

	private native void n_onAudioDecoderInitialized (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3);


	public void onAudioDecoderReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onAudioDecoderReleased (p0, p1);
	}

	private native void n_onAudioDecoderReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);


	public void onAudioDisabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1)
	{
		n_onAudioDisabled (p0, p1);
	}

	private native void n_onAudioDisabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1);


	public void onAudioEnabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1)
	{
		n_onAudioEnabled (p0, p1);
	}

	private native void n_onAudioEnabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1);


	public void onAudioInputFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Format p1, com.google.android.exoplayer2.decoder.DecoderReuseEvaluation p2)
	{
		n_onAudioInputFormatChanged (p0, p1, p2);
	}

	private native void n_onAudioInputFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Format p1, com.google.android.exoplayer2.decoder.DecoderReuseEvaluation p2);


	public void onAudioPositionAdvancing (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onAudioPositionAdvancing (p0, p1);
	}

	private native void n_onAudioPositionAdvancing (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1);


	public void onAudioSessionIdChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onAudioSessionIdChanged (p0, p1);
	}

	private native void n_onAudioSessionIdChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1);


	public void onAudioSinkError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onAudioSinkError (p0, p1);
	}

	private native void n_onAudioSinkError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);


	public void onAudioUnderrun (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3)
	{
		n_onAudioUnderrun (p0, p1, p2, p3);
	}

	private native void n_onAudioUnderrun (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3);


	public void onAvailableCommandsChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Player.Commands p1)
	{
		n_onAvailableCommandsChanged (p0, p1);
	}

	private native void n_onAvailableCommandsChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Player.Commands p1);


	public void onBandwidthEstimate (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3)
	{
		n_onBandwidthEstimate (p0, p1, p2, p3);
	}

	private native void n_onBandwidthEstimate (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3);


	public void onCues (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.text.CueGroup p1)
	{
		n_onCues (p0, p1);
	}

	private native void n_onCues (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.text.CueGroup p1);


	public void onDecoderDisabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, com.google.android.exoplayer2.decoder.DecoderCounters p2)
	{
		n_onDecoderDisabled (p0, p1, p2);
	}

	private native void n_onDecoderDisabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, com.google.android.exoplayer2.decoder.DecoderCounters p2);


	public void onDecoderEnabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, com.google.android.exoplayer2.decoder.DecoderCounters p2)
	{
		n_onDecoderEnabled (p0, p1, p2);
	}

	private native void n_onDecoderEnabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, com.google.android.exoplayer2.decoder.DecoderCounters p2);


	public void onDecoderInitialized (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, java.lang.String p2, long p3)
	{
		n_onDecoderInitialized (p0, p1, p2, p3);
	}

	private native void n_onDecoderInitialized (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, java.lang.String p2, long p3);


	public void onDecoderInputFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, com.google.android.exoplayer2.Format p2)
	{
		n_onDecoderInputFormatChanged (p0, p1, p2);
	}

	private native void n_onDecoderInputFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, com.google.android.exoplayer2.Format p2);


	public void onDeviceInfoChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.DeviceInfo p1)
	{
		n_onDeviceInfoChanged (p0, p1);
	}

	private native void n_onDeviceInfoChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.DeviceInfo p1);


	public void onDeviceVolumeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, boolean p2)
	{
		n_onDeviceVolumeChanged (p0, p1, p2);
	}

	private native void n_onDeviceVolumeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, boolean p2);


	public void onDownstreamFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.MediaLoadData p1)
	{
		n_onDownstreamFormatChanged (p0, p1);
	}

	private native void n_onDownstreamFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.MediaLoadData p1);


	public void onDrmKeysLoaded (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmKeysLoaded (p0);
	}

	private native void n_onDrmKeysLoaded (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0);


	public void onDrmKeysRemoved (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmKeysRemoved (p0);
	}

	private native void n_onDrmKeysRemoved (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0);


	public void onDrmKeysRestored (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmKeysRestored (p0);
	}

	private native void n_onDrmKeysRestored (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0);


	public void onDrmSessionAcquired (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onDrmSessionAcquired (p0, p1);
	}

	private native void n_onDrmSessionAcquired (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1);


	public void onDrmSessionManagerError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onDrmSessionManagerError (p0, p1);
	}

	private native void n_onDrmSessionManagerError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);


	public void onDrmSessionReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmSessionReleased (p0);
	}

	private native void n_onDrmSessionReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0);


	public void onDroppedVideoFrames (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, long p2)
	{
		n_onDroppedVideoFrames (p0, p1, p2);
	}

	private native void n_onDroppedVideoFrames (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, long p2);


	public void onEvents (com.google.android.exoplayer2.Player p0, com.google.android.exoplayer2.analytics.AnalyticsListener.Events p1)
	{
		n_onEvents (p0, p1);
	}

	private native void n_onEvents (com.google.android.exoplayer2.Player p0, com.google.android.exoplayer2.analytics.AnalyticsListener.Events p1);


	public void onIsLoadingChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onIsLoadingChanged (p0, p1);
	}

	private native void n_onIsLoadingChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1);


	public void onIsPlayingChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onIsPlayingChanged (p0, p1);
	}

	private native void n_onIsPlayingChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1);


	public void onLoadCanceled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2)
	{
		n_onLoadCanceled (p0, p1, p2);
	}

	private native void n_onLoadCanceled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2);


	public void onLoadCompleted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2)
	{
		n_onLoadCompleted (p0, p1, p2);
	}

	private native void n_onLoadCompleted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2);


	public void onLoadError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2, java.io.IOException p3, boolean p4)
	{
		n_onLoadError (p0, p1, p2, p3, p4);
	}

	private native void n_onLoadError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2, java.io.IOException p3, boolean p4);


	public void onLoadStarted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2)
	{
		n_onLoadStarted (p0, p1, p2);
	}

	private native void n_onLoadStarted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.LoadEventInfo p1, com.google.android.exoplayer2.source.MediaLoadData p2);


	public void onLoadingChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onLoadingChanged (p0, p1);
	}

	private native void n_onLoadingChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1);


	public void onMaxSeekToPreviousPositionChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onMaxSeekToPreviousPositionChanged (p0, p1);
	}

	private native void n_onMaxSeekToPreviousPositionChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1);


	public void onMediaItemTransition (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.MediaItem p1, int p2)
	{
		n_onMediaItemTransition (p0, p1, p2);
	}

	private native void n_onMediaItemTransition (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.MediaItem p1, int p2);


	public void onMediaMetadataChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.MediaMetadata p1)
	{
		n_onMediaMetadataChanged (p0, p1);
	}

	private native void n_onMediaMetadataChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.MediaMetadata p1);


	public void onMetadata (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.metadata.Metadata p1)
	{
		n_onMetadata (p0, p1);
	}

	private native void n_onMetadata (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.metadata.Metadata p1);


	public void onPlayWhenReadyChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2)
	{
		n_onPlayWhenReadyChanged (p0, p1, p2);
	}

	private native void n_onPlayWhenReadyChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2);


	public void onPlaybackParametersChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.PlaybackParameters p1)
	{
		n_onPlaybackParametersChanged (p0, p1);
	}

	private native void n_onPlaybackParametersChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.PlaybackParameters p1);


	public void onPlaybackStateChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onPlaybackStateChanged (p0, p1);
	}

	private native void n_onPlaybackStateChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1);


	public void onPlaybackSuppressionReasonChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onPlaybackSuppressionReasonChanged (p0, p1);
	}

	private native void n_onPlaybackSuppressionReasonChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1);


	public void onPlayerError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.PlaybackException p1)
	{
		n_onPlayerError (p0, p1);
	}

	private native void n_onPlayerError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.PlaybackException p1);


	public void onPlayerErrorChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.PlaybackException p1)
	{
		n_onPlayerErrorChanged (p0, p1);
	}

	private native void n_onPlayerErrorChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.PlaybackException p1);


	public void onPlayerReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0)
	{
		n_onPlayerReleased (p0);
	}

	private native void n_onPlayerReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0);


	public void onPlayerStateChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2)
	{
		n_onPlayerStateChanged (p0, p1, p2);
	}

	private native void n_onPlayerStateChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2);


	public void onPlaylistMetadataChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.MediaMetadata p1)
	{
		n_onPlaylistMetadataChanged (p0, p1);
	}

	private native void n_onPlaylistMetadataChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.MediaMetadata p1);


	public void onPositionDiscontinuity (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Player.PositionInfo p1, com.google.android.exoplayer2.Player.PositionInfo p2, int p3)
	{
		n_onPositionDiscontinuity (p0, p1, p2, p3);
	}

	private native void n_onPositionDiscontinuity (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Player.PositionInfo p1, com.google.android.exoplayer2.Player.PositionInfo p2, int p3);


	public void onRenderedFirstFrame (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Object p1, long p2)
	{
		n_onRenderedFirstFrame (p0, p1, p2);
	}

	private native void n_onRenderedFirstFrame (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Object p1, long p2);


	public void onRepeatModeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onRepeatModeChanged (p0, p1);
	}

	private native void n_onRepeatModeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1);


	public void onSeekBackIncrementChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onSeekBackIncrementChanged (p0, p1);
	}

	private native void n_onSeekBackIncrementChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1);


	public void onSeekForwardIncrementChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onSeekForwardIncrementChanged (p0, p1);
	}

	private native void n_onSeekForwardIncrementChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1);


	public void onSeekProcessed (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0)
	{
		n_onSeekProcessed (p0);
	}

	private native void n_onSeekProcessed (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0);


	public void onSeekStarted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0)
	{
		n_onSeekStarted (p0);
	}

	private native void n_onSeekStarted (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0);


	public void onShuffleModeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onShuffleModeChanged (p0, p1);
	}

	private native void n_onShuffleModeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1);


	public void onSkipSilenceEnabledChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onSkipSilenceEnabledChanged (p0, p1);
	}

	private native void n_onSkipSilenceEnabledChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, boolean p1);


	public void onSurfaceSizeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, int p2)
	{
		n_onSurfaceSizeChanged (p0, p1, p2);
	}

	private native void n_onSurfaceSizeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1, int p2);


	public void onTimelineChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onTimelineChanged (p0, p1);
	}

	private native void n_onTimelineChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, int p1);


	public void onTrackSelectionParametersChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.trackselection.TrackSelectionParameters p1)
	{
		n_onTrackSelectionParametersChanged (p0, p1);
	}

	private native void n_onTrackSelectionParametersChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.trackselection.TrackSelectionParameters p1);


	public void onTracksChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Tracks p1)
	{
		n_onTracksChanged (p0, p1);
	}

	private native void n_onTracksChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Tracks p1);


	public void onUpstreamDiscarded (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.MediaLoadData p1)
	{
		n_onUpstreamDiscarded (p0, p1);
	}

	private native void n_onUpstreamDiscarded (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.source.MediaLoadData p1);


	public void onVideoCodecError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onVideoCodecError (p0, p1);
	}

	private native void n_onVideoCodecError (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);


	public void onVideoDecoderInitialized (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3)
	{
		n_onVideoDecoderInitialized (p0, p1, p2, p3);
	}

	private native void n_onVideoDecoderInitialized (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3);


	public void onVideoDecoderReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onVideoDecoderReleased (p0, p1);
	}

	private native void n_onVideoDecoderReleased (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);


	public void onVideoDisabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1)
	{
		n_onVideoDisabled (p0, p1);
	}

	private native void n_onVideoDisabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1);


	public void onVideoEnabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1)
	{
		n_onVideoEnabled (p0, p1);
	}

	private native void n_onVideoEnabled (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.decoder.DecoderCounters p1);


	public void onVideoFrameProcessingOffset (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1, int p2)
	{
		n_onVideoFrameProcessingOffset (p0, p1, p2);
	}

	private native void n_onVideoFrameProcessingOffset (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, long p1, int p2);


	public void onVideoInputFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Format p1, com.google.android.exoplayer2.decoder.DecoderReuseEvaluation p2)
	{
		n_onVideoInputFormatChanged (p0, p1, p2);
	}

	private native void n_onVideoInputFormatChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.Format p1, com.google.android.exoplayer2.decoder.DecoderReuseEvaluation p2);


	public void onVideoSizeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.video.VideoSize p1)
	{
		n_onVideoSizeChanged (p0, p1);
	}

	private native void n_onVideoSizeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, com.google.android.exoplayer2.video.VideoSize p1);


	public void onVolumeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, float p1)
	{
		n_onVolumeChanged (p0, p1);
	}

	private native void n_onVolumeChanged (com.google.android.exoplayer2.analytics.AnalyticsListener.EventTime p0, float p1);

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
