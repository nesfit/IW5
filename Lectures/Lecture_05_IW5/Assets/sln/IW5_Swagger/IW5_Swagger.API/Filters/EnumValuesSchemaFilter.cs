using System;
using System.Collections.Generic;
using System.Linq;
using IW5_Swagger.API.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IW5_Swagger.API.Filters
{
    public class EnumValuesSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (context.SystemType.IsEnum)
            {
                IList<EnumValueSchemaModel> enumValues = new List<EnumValueSchemaModel>();

                var values = Enum.GetValues(context.SystemType);
                foreach (var value in values)
                {
                    enumValues.Add(new EnumValueSchemaModel
                    {
                        Name = value.ToString(),
                        Value = Convert.ToInt32(value)
                    });
                }

                schema.Enum = new List<object>();
                var intValues = enumValues.Select(enumValue => enumValue.Value);
                foreach (var value in intValues)
                {
                    schema.Enum.Add(value);
                }
                schema.Extensions.Add(
                    "x-ms-enum",
                    new
                    {
                        name = context.SystemType.Name,
                        modelAsString = false,
                        values = enumValues.ToArray()
                    });
            }
        }
    }
}