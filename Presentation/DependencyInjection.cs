using Infrastructure.Authentification;
using Microsoft.OpenApi.Models;
using Presentation.Filter;
using Presentation.Modelbinding.Provider;
using Presentation.Modelbinding.Provider.HttpQuery;
using Presentation.Swagger.DocumentFilter;
using Presentation.Swagger.OperationFilter;
using Presentation.Swagger.Options;
using Presentation.Swagger.SignalR;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.Const;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using Shared.Json;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddAuthentication("Base").
                AddScheme<BasicAuthenticationOptions, AuthentificationHandler>("Base", null);
            services.AddAuthorization(options =>
            {
                //User Policy: Any user with the role root
                options.AddPolicy(AuthorizationConst.Policy.RootPolicy, policy =>
                                  policy.RequireClaim(AuthorizationConst.Claims.ClaimTypeIsRoot, bool.TrueString));
                //User Policy: Any user with the role admin
                options.AddPolicy(AuthorizationConst.Policy.AdminPolicy, policy =>
                                  policy.RequireClaim(AuthorizationConst.Claims.ClaimTypeIsAdmin, bool.TrueString));
                //User Policy: Any registered User that confirms his registration
                options.AddPolicy(AuthorizationConst.Policy.UserPolicy, policy =>
                                  policy.RequireClaim(AuthorizationConst.Claims.ClaimTypeIsActivatedUser, bool.TrueString));
            });
            services.AddControllers(x => {
                //Filters
                x.Filters.Add<PrepareResponseFailoverFilter>(int.MinValue+1);//wandelt ObjectResult von Action in Ziel Json Struktur (ApiResponse<T>)
                x.Filters.Add<PrepareResponseFilter>(int.MinValue);//wandelt ObjectResult von Action in Ziel Json Struktur (ApiResponse<T>)
                //ModelBinder
                x.ModelBinderProviders.Insert(0, new QueryParametersModelBinderProvider());//Für Query Param falls gesetzt
                x.ModelBinderProviders.Insert(1, new BodyModelBinderProvider());//Für Query Param falls gesetzt
            })
                .AddApplicationPart(WebApi.AssemblyReference.Assembly)
                .AddJsonOptions(x => {
                    x.JsonSerializerOptions.PropertyNamingPolicy = new LowerCaseNamingPolicy() ;
                    x.JsonSerializerOptions.WriteIndented = true;
                    x.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => 
                { 
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {

                        Version = "v1",
                        Title = "Jellyfish Multiplatform Mobile App (iOS/Android)",
                        Description = "Backend for the Jellyfish App",
                        TermsOfService = new Uri("https://jellyfish.mail.net/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Mika Roos",
                            Url = new Uri("https://jellyfish.mail.net/contact")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "GPL2",
                            Url = new Uri("https://jellyfish.mail.net/license")
                        }
                    });
                    #region Api Key Header append for Request via Swagger UI

                    options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Description = "JWT Bearer",
                        Scheme = "Base"
                    });
                    var key = new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Authorization"
                        },
                        In = ParameterLocation.Header
                    };

                    var requirement = new OpenApiSecurityRequirement
                    {
                        {key, new List<string>() }
                    };
                    options.AddSecurityRequirement(requirement);
                    #endregion
                    options.ExampleFilters();
                    options.OperationFilter<OpenApiIgnoreMethodParameterOperationFilter>();
                    options.DocumentFilter<SignalRDocumentationFilter>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiDataTransferObject<object>>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiDataTransferObject<UserDTO>>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiDataTransferObject<List<UserDTO>>>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiDataTransferObject<ChatDTO>>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiDataTransferObject<RoleDTO>>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiData<object>>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiError>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<List<ApiError>>>();
                    options.DocumentFilter<PolymorphismDocumentFilter<ApiMeta>>();
                    options.GenerateCustomRequestBodyForApiDataTransferObject();//Wraps the ApiDataTransferObject<T> around each IDataTransferObject concrete Model like Share.DataTransferObject.UserDTO
                    
                }
            ); 
            services.AddSwaggerExamples();
            return services;
        }
    }
}
