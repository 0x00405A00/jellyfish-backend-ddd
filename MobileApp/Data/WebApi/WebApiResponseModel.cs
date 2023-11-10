using MobileApp.Handler.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MobileApp.Data.WebApi
{
    public class WebApiModel
    {
        public class RootObject
        {
            public object? data { get; set; }
            public Meta? meta { get; set; }
            public Jsonapi? jsonapi { get; set; }
            public Error[]? errors { get; set; }




        }
        public class RootObject<T> : RootObject
            where T : class
        {
            public new Data<T>[]? data { get; set; }
        }

        public class Meta
        {
            public int? count { get; set; }
            public string? message { get; set; }
            public string? debugmessage { get; set; }
            public DebugObject? debugobject { get; set; }
        }

        public class DebugObject
        {
        }

        public class Error
        {
            public Links? links { get; set; }
            public string? status { get; set; }
            public int? code { get; set; }
            public string? title { get; set; }
            public string? detail { get; set; }
            public ErrorSource? source { get; set; }
        }
        public class ErrorSource
        {

            public string? pointer { get; set; }
            public string? parameter { get; set; }
        }
        public class Jsonapi
        {
            public string? version { get; set; }
            public string? company { get; set; }
            public string? author { get; set; }
            public string? copyright { get; set; }
            public string? use { get; set; }
            public string? rfc { get; set; }
        }

        public class Data<T>
        {
            public string? id { get; set; }
            public string? type { get; set; }
            public T? attributes { get; set; }
            public Links? links { get; set; }
            public int? depth { get; set; }
            public object? relationships { get; set; }
            public Included<T>[]? included { get; set; }
            public Meta? meta { get; set; }
        }

        public class Data
        {
            public string? id { get; set; }
            public string? type { get; set; }
            public int? depth { get; set; }
            public Meta? meta { get; set; }
        }

        public class Included<T>
        {
            public string? id { get; set; }
            public string? type { get; set; }
            public T? attributes { get; set; }
            public Links? links { get; set; }
            public int? depth { get; set; }
            public Meta? meta { get; set; }
        }

        public class Links
        {
            public string? self { get; set; }
            public string? last { get; set; }
            public string? previous { get; set; }
            public string? next { get; set; }
            public Meta? Meta { get; set; }
        }

    }

}
