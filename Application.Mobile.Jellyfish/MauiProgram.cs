using Camera.MAUI;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using MobileApp.ApplicationSpecific;
using MobileApp.Handler.AppConfig;
using MobileApp.Service;
using SkiaSharp.Views.Maui.Controls.Hosting;
/*using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;*/

namespace MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();


            var appConfigHandler = ApplicationConfigHandlerSingleton.Get();
            appConfigHandler.ApplicationConfig = appConfigHandler.Load();

            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkitMediaElement()//Integrated Media Player UI Element for Videorecoding Playback
                .UseMauiCameraView()//Camera MAUI Lib Init
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldSuppressExceptionsInConverters(false);
                    options.SetShouldSuppressExceptionsInBehaviors(false);
                    options.SetShouldSuppressExceptionsInAnimations(false);
                })
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events => {

#if ANDROID
                    events.AddAndroid(android => android
                        .OnDestroy(e => appConfigHandler.Safe()));//ApplicationExit Event like (wenn Handy Ressourcen selbst freigibt oder der User die App terminated)
#elif IOS

                    events.AddiOS(ios => ios
                        .WillTerminate(del => appConfigHandler.Safe()));//ApplicationExit Event like (wenn Handy Ressourcen selbst freigibt oder der User die App terminated)
#endif
                })
                /*.UseLocalNotification(config =>
                {
                    config.AddCategory(new NotificationCategory(NotificationCategoryType.Status)
                    {
                        ActionList = new HashSet<NotificationAction>(new List<NotificationAction>()
                        {
                            new NotificationAction(100)
                            {
                                Title = "Launch",
                                Android =
                                {
                                    LaunchAppWhenTapped = true,
                                    IconName =
                                    {
                                        ResourceName = "i2"
                                    }
                                }
                            },
                            new NotificationAction(101)
                            {
                                Title = "Close",
                                Android =
                                {
                                     
                                    LaunchAppWhenTapped = false,
                                    IconName =
                                    {
                                        ResourceName = "i3"
                                    }
                                }
                            }
                        })
                    })
                    .AddAndroid(android =>
                    {
                        android.AddChannel(new NotificationChannelRequest
                        {
                            Sound = "good_things_happen"
                        });
                    })
                    .AddiOS(iOS =>
                    {
#if IOS
                    iOS.UseCustomDelegate = true;
                    iOS.SetCustomUserNotificationCenterDelegate(new CustomUserNotificationCenterDelegate());
#endif
                    });
                });*/ ;

#if DEBUG
		builder.Logging.AddDebug();
#endif
            //Pages können aktuell nicht in die DI injected werden, da es zu Problemen mit AppResouces auf dem Resources/* und ResourceDictionaries kommen kann, dass
            //Resource zur Laufzeit nicht gefunden werden.
            //Das Problem ist das die Pages in die DI gepackt werden ohne das die Resources überhaupt verfügbar sind. Diese sind erst nach dem builder.Build() verfügbar.


#if __ANDROID__//Fixxed bei einer CollectionView in der man Images auswählen kann folgenden Fehler: Canvas: trying to use a recycled bitmap
        ImageHandler.Mapper.PrependToMapping(nameof(Microsoft.Maui.IImage.Source), (handler, view) => PrependToMappingImageSource(handler, view));
#endif


            builder.Services.AddSingleton<JsonHandler>();
            builder.Services.AddSingleton(appConfigHandler);


            builder.Services.AddSqlLiteDatabase(Global.DatabasePath, Global.DatabaseFlags);

            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddDeviceHandlers(appConfigHandler);
            builder.Services.AddViewModels();
            builder.Services.AddPages();
            return builder.Build();
        }
#if __ANDROID__//Fixxed bei einer CollectionView in der man Images auswählen kann folgenden Fehler: Canvas: trying to use a recycled bitmap
    public static void PrependToMappingImageSource(IImageHandler handler, Microsoft.Maui.IImage image)
    {
        handler.PlatformView?.Clear();
    }
#endif


    }
}