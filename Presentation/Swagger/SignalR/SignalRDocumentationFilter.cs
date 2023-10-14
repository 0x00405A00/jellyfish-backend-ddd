using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace Presentation.Swagger.SignalR
{
    public class SignalRDocumentationFilter : IDocumentFilter
    {
        private readonly List<EndpointDataSource> _endpoints;
        public SignalRDocumentationFilter(IEnumerable<EndpointDataSource> endpointSources)
        {
            _endpoints = endpointSources.ToList();
        }
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var endpoints = _endpoints.SelectMany(es => es.Endpoints).OfType<RouteEndpoint>().ToList();
            /*foreach (var item in endpoints)
            {
                var hubServiceAttr = item.Metadata.OfType<Hub>();
                if (!hubServiceAttr.Any())
                {
                    continue;
                }
                var hubMeta = item.Metadata.OfType<HubMetadata>().First();
                var t = item.GetType();

                string route = item.RoutePattern.RawText;

                string hubName = hubServiceAttr.First().Route.TrimStart('/');
                string tagName = "SignalR/WebSocket Endpoint - " + hubName[0].ToString().ToUpper() + hubName.Substring(1, hubName.Length - 1);
                OpenApiPathItem openApiPathItem = new OpenApiPathItem();
                var hubMethods = hubMeta.HubType.GetMethods().ToList().FindAll(x => x.IsPublic && x.GetBaseDefinition() == null);

                SignalRInitMessage signalRInitMessage = new SignalRInitMessage() { protocol = "json", version = 1 };
                string contentInit = JsonSerializer.Serialize(signalRInitMessage);
                var encodingDict = new Dictionary<string, OpenApiEncoding>() { { "UTF8", new OpenApiEncoding { ContentType = "application/json" } } };
                openApiPathItem.AddOperation(OperationType.Get, new OpenApiOperation
                {
                    RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                             {
                                 "application/json",new OpenApiMediaType
                                 {
                                     Encoding=encodingDict,
                                 }
                             }
                        },
                    },
                    Description = "Init",
                    Tags = new List<OpenApiTag>()
                        {

                            new OpenApiTag
                            {
                                Name= tagName
                            }
                        }
                });
                foreach (var action in hubMethods)
                {
                    var p = action.GetParameters().ToList();
                    var args = new string[p?.Count ?? 0];
                    if (p != null && p.Any())
                    {
                        for (int i = 0; i < p.Count; i++)
                        {
                            args[i] = p[i].ToString();
                        }
                    }
                    SignalRInvokeMethodModel signalRInvokeMethodModel = new SignalRInvokeMethodModel() { arguments = args, invocationId = "0", target = action.Name, type = 0 };
                    string contentAction = JsonSerializer.Serialize(signalRInvokeMethodModel);
                    openApiPathItem.AddOperation(OperationType.Get, new OpenApiOperation
                    {
                        RequestBody = new OpenApiRequestBody
                        {
                            Content = new Dictionary<string, OpenApiMediaType>
                        {
                             {
                                 "application/json",new OpenApiMediaType
                                 {
                                     Encoding=encodingDict,
                                 }
                             }
                        },
                        },
                        Description = action.Name,
                        Tags = new List<OpenApiTag>()
                        {

                            new OpenApiTag
                            {
                                Name= tagName
                            }
                        }
                    });
                }
                openApiPathItem.Description = route;
                openApiPathItem.Summary = "SignalR Hub Method";


                if (!swaggerDoc.Paths.ContainsKey(route))
                {
                    swaggerDoc.Paths.Add(route, openApiPathItem);
                }
            }*/
        }

        public class SignalRInitMessage
        {
            public string protocol { get; set; }
            public int version { get; set; }
        }

        public class SignalRInvokeMethodModel
        {
            public string[] arguments { get; set; }
            public string invocationId { get; set; }
            public string target { get; set; }
            public int type { get; set; }
        }

    }
}
