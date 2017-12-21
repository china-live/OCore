using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XCore.Mvc.Core.ModelBinding
{
    public class CheckMarkModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (bindingContext.ModelType == typeof(bool))
            {
                var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueProviderResult == ValueProviderResult.None)
                {
                    return Task.CompletedTask;
                }

                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                if (valueProviderResult.Values == "✓")
                {
                    bindingContext.Result = ModelBindingResult.Success(bindingContext.ModelName);
                }
                else if (valueProviderResult.Values == "✗")
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                }
            }

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// An <see cref="IModelBinderProvider"/> for <see cref="CheckMarkModelBinder"/>
    /// </summary>
    public class CheckMarkModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc />
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(CheckMarkModelBinder))
            {
                return new CheckMarkModelBinder();
            }

            return null;
        }
    }
}
