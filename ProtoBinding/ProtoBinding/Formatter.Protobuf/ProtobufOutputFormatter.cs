﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using ProtoBuf;
using ProtoBuf.Meta;


namespace ProtoBinding.Formatter.Protobuf
{
  public class ProtobufOutputFormatter:OutputFormatter
  {
    private readonly ProtobufFormatterOptions _options;

    public ProtobufOutputFormatter(ProtobufFormatterOptions protobufFormatterOptions)
    {
      ContentType = "application/x-protobuf";
      _options = protobufFormatterOptions ?? throw new ArgumentNullException(nameof(protobufFormatterOptions));
      foreach (var contentType in protobufFormatterOptions.SupportedContentTypes)
      {
        SupportedMediaTypes.Add(new MediaTypeHeaderValue(contentType));
      }
    }

    private static readonly Lazy<RuntimeTypeModel> model = new Lazy<RuntimeTypeModel>(CreateTypeModel);

    public string ContentType { get; private set; }

    public static RuntimeTypeModel Model => model.Value;

    private static RuntimeTypeModel CreateTypeModel()
    {
      var typeModel = TypeModel.Create();
      typeModel.UseImplicitZeroDefaults = false;
      return typeModel;
    }

    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
    {
      var response = context.HttpContext.Response;
      Model.Serialize(response.Body, context.Object);
      return Task.FromResult(response);
    }


    
  }
}
