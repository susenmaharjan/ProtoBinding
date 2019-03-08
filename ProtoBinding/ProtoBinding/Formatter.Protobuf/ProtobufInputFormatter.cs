﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using ProtoBuf.Meta;

namespace ProtoBinding.Formatter.Protobuf
{
  public class ProtobufInputFormatter:InputFormatter
  {

    private readonly ProtobufFormatterOptions _options;

    public ProtobufInputFormatter(ProtobufFormatterOptions protobufFormatterOptions)
    {
      _options = protobufFormatterOptions ?? throw new ArgumentNullException(nameof(protobufFormatterOptions));
      foreach (var contentType in protobufFormatterOptions.SupportedContentTypes)
      {
        SupportedMediaTypes.Add(new MediaTypeHeaderValue(contentType));
      }
    }

    private static readonly Lazy<RuntimeTypeModel> model = new Lazy<RuntimeTypeModel>(CreateTypeModel);

    public static RuntimeTypeModel Model => model.Value;

    public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
      var type = context.ModelType;
      var request = context.HttpContext.Request;
      MediaTypeHeaderValue requestContentType = null;
      MediaTypeHeaderValue.TryParse(request.ContentType, out requestContentType);


      object result = Model.Deserialize(context.HttpContext.Request.Body, null, type);
      return InputFormatterResult.SuccessAsync(result);
    }

    protected override bool CanReadType(Type type)
    {
      return true;
    }

    private static RuntimeTypeModel CreateTypeModel()
    {
      var typeModel = TypeModel.Create();
      typeModel.UseImplicitZeroDefaults = false;
      return typeModel;
    }
  }
}
