using OCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection {
    public static class OCoreBuilderExtensions {
        /// <summary>
        /// Adds tenant level MVC services and configuration.
        /// </summary>
        public static OCoreBuilder AddMvc(this OCoreBuilder builder) {
            return builder.RegisterStartup<Startup>();
        }
    }
}
