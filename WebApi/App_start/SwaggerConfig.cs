using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing.Constraints;
using Swashbuckle.Application;

namespace Payoneer.Payoneer.Hotels.WebApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfig
    {
        public static void Register(HttpConfiguration httpConfig)
        {
            httpConfig
                .EnableSwagger(c =>
                {
                    // If schemes are not explicitly provided in a Swagger 2.0 document, then the scheme used to access
                    // the docs is taken as the default. If your API supports multiple schemes and you want to be explicit
                    // about them, you can use the "Schemes" option as shown below.
                    //
                    c.Schemes(new[] { "http", "https" });

                    // Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
                    // hold additional metadata for an API. Version and title are required but you may also provide the
                    // additional fields.
                    //
                    //TODO: [NewApp] Update description
                    c.SingleApiVersion("v1", "Swashbuckle Dummy")
                        .Description("A sample API for testing and prototyping Swashbuckle features");

                    // You can use "BasicAuth", "ApiKey" or "OAuth2" options to describe security schemes for the API.
                    // See https://github.com/swagger-api/swagger-spec/blob/master/versions/V2.md for more details.
                    // NOTE: These only define the schemes and need to be coupled with a corresponding "security" property
                    // at the document or operation level to indicate which schemes are required for an operation. To do this,
                    // you'll need to implement a custom IDocumentFilter and/or IOperationFilter to set these properties
                    // according to your specific authorization implementation

                    c.OAuth2("oauth2")
                        .Description("OAuth2 Implicit Grant")
                        .Flow("implicit")
                        .AuthorizationUrl("http://petstore.swagger.io/oauth/dialog")
                        //.TokenUrl("https://tempuri.org/token")
                        .Scopes(scopes => {
                            scopes.Add("read", "Read access to protected resources");
                            scopes.Add("write", "Write access to protected resources");
                        });

                    // Set this flag to omit descriptions for any actions decorated with the Obsolete attribute
                    //c.IgnoreObsoleteActions();

                    // Each operation be assigned one or more tags which are then used by consumers for various reasons.
                    // For example, the swagger-ui groups operations according to the first tag of each operation.
                    // By default, this will be controller name but you can use the "GroupActionsBy" option to
                    // override with any value.
                    //
                    //c.GroupActionsBy(apiDesc => apiDesc.HttpMethod.ToString());

                    // You can also specify a custom sort order for groups (as defined by "GroupActionsBy") to dictate
                    // the order in which operations are listed. For example, if the default grouping is in place
                    // (controller name) and you specify a descending alphabetic sort order, then actions from a
                    // ProductsController will be listed before those from a CustomersController. This is typically
                    // used to customize the order of groupings in the swagger-ui.
                    //
                    //c.OrderActionGroupsBy(new DescendingAlphabeticComparer());

                    // If you annonate Controllers and API Types with
                    // Xml comments (http://msdn.microsoft.com/en-us/library/b2s063f7(v=vs.110).aspx), you can incorporate
                    // those comments into the generated docs and UI. You can enable this by providing the path to one or
                    // more Xml comment files.
                    //
                    c.IncludeXmlComments(GetXmlCommentsPath("Payoneer.Payoneer.Hotels.WebApi.xml"));
                    c.IncludeXmlComments(GetXmlCommentsPath("Payoneer.ServicesInfra.Controllers.xml"));
                    c.IncludeXmlComments(GetXmlCommentsPath("PubComp.Caching.WebApiExtended.xml"));

                    // Swashbuckle makes a best attempt at generating Swagger compliant JSON schemas for the various types
                    // exposed in your API. However, there may be occasions when more control of the output is needed.
                    // This is supported through the MapType and SchemaFilter options:
                    //
                    // Use the "MapType" option to override the Schema generation for a specific type.
                    // It should be noted that the resulting Schema will be placed "inline" for any applicable Operations.
                    // While Swagger 2.0 supports inline definitions for "all" Schema types, the swagger-ui tool does not.
                    // It expects "complex" Schemas to be defined separately and referenced. For this reason, you should only
                    // use the MapType option when the resulting Schema is a primitive or array type. If you need to alter a
                    // complex Schema, use a Schema filter.
                    //
                    //c.MapType<ProductType>(() => new Schema { type = "integer", format = "int32" });

                    // If you want to post-modify "complex" Schemas once they've been generated, across the board or for a
                    // specific type, you can wire up one or more Schema filters.
                    //
                    //c.SchemaFilter<ApplySchemaVendorExtensions>();

                    // In a Swagger 2.0 document, complex types are typically declared globally and referenced by unique
                    // Schema Id. By default, Swashbuckle does NOT use the full type name in Schema Ids. In most cases, this
                    // works well because it prevents the "implementation detail" of type namespaces from leaking into your
                    // Swagger docs and UI. However, if you have multiple types in your API with the same class name, you'll
                    // need to opt out of this behavior to avoid Schema Id conflicts.
                    //
                    //c.UseFullTypeNameInSchemaIds();

                    // Alternatively, you can provide your own custom strategy for inferring SchemaId's for
                    // describing "complex" types in your API.
                    //  
                    c.SchemaId(t => (t.FullName?.Contains('`') ?? false) ? t.FullName.Substring(0, t.FullName.IndexOf('`')) : t.FullName);

                    // Set this flag to omit schema property descriptions for any type properties decorated with the
                    // Obsolete attribute 
                    c.IgnoreObsoleteProperties();

                    // In accordance with the built in JsonSerializer, Swashbuckle will, by default, describe enums as integers.
                    // You can change the serializer behavior by configuring the StringToEnumConverter globally or for a given 
                    // enum type. Swashbuckle will honor this change out-of-the-box. However, if you use a different
                    // approach to serialize enums as strings, you can also force Swashbuckle to describe them as strings.
                    // 
                    c.DescribeAllEnumsAsStrings();

                    // Similar to Schema filters, Swashbuckle also supports Operation and Document filters:
                    //
                    // Post-modify Operation descriptions once they've been generated by wiring up one or more
                    // Operation filters.
                    //
                    //c.OperationFilter<AddDefaultResponse>();
                    //
                    // If you've defined an OAuth2 flow as described above, you could use a custom filter
                    // to inspect some attribute on each action and infer which (if any) OAuth2 scopes are required
                    // to execute the operation
                    //
                    //c.OperationFilter<AssignOAuth2SecurityRequirements>();
                    //c.OperationFilter<AddFileUploadParams>();
                    //c.OperationFilter<SupportFlaggedEnums>();
                    //c.OperationFilter<UpdateFileDownloadOperations>();

                    // Post-modify the entire Swagger document by wiring up one or more Document filters.
                    // This gives full control to modify the final SwaggerDocument. You should have a good understanding of
                    // the Swagger 2.0 spec. - https://github.com/swagger-api/swagger-spec/blob/master/versions/V2.md
                    // before using this option.
                    //
                    //c.DocumentFilter<ApplyDocumentVendorExtensions>();
                    //
                    //c.DocumentFilter<AppendVersionToBasePath>();

                    // In contrast to WebApi, Swagger 2.0 does not include the query string component when mapping a URL
                    // to an action. As a result, Swashbuckle will raise an exception if it encounters multiple actions
                    // with the same path (sans query string) and HTTP method. You can workaround this by providing a
                    // custom strategy to pick a winner or merge the descriptions for the purposes of the Swagger docs 
                    //
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    // Wrap the default SwaggerGenerator with additional behavior (e.g. caching) or provide an
                    // alternative implementation for ISwaggerProvider with the CustomProvider option.
                    //
                    //c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));
                })
                .EnableSwaggerUi(c =>
                {
                    // Use the "InjectStylesheet" option to enrich the UI with one or more additional CSS stylesheets.
                    // The file must be included in your project as an "Embedded Resource", and then the resource's
                    // "Logical Name" is passed to the method as shown below.
                    //
                    //c.InjectStylesheet(containingAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testStyles1.css");

                    // Use the "InjectJavaScript" option to invoke one or more custom JavaScripts after the swagger-ui
                    // has loaded. The file must be included in your project as an "Embedded Resource", and then the resource's
                    // "Logical Name" is passed to the method as shown above.
                    //
                    //c.InjectJavaScript(thisAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testScript1.js");

                    // By default, swagger-ui will validate specs against swagger.io's online validator and display the result
                    // in a badge at the bottom of the page. Use these options to set a different validator URL or to disable the
                    // feature entirely.
                    //c.SetValidatorUrl("http://localhost/validator");
                    c.DisableValidator();

                    // Use this option to control how the Operation listing is displayed.
                    // It can be set to "None" (default), "List" (shows operations for each resource),
                    // or "Full" (fully expanded: shows operations and their details).
                    //
                    c.DocExpansion(DocExpansion.List);

                    // Specify which HTTP operations will have the 'Try it out!' option. An empty paramter list disables
                    // it for all operations.
                    //
                    //c.SupportedSubmitMethods("GET", "HEAD");

                    // Use the CustomAsset option to provide your own version of assets used in the swagger-ui.
                    // It's typically used to instruct Swashbuckle to return your version instead of the default
                    // when a request is made for "index.html". As with all custom content, the file must be included
                    // in your project as an "Embedded Resource", and then the resource's "Logical Name" is passed to
                    // the method as shown below.
                    //
                    //c.CustomAsset("index.html", containingAssembly, "YourWebApiProject.SwaggerExtensions.index.html");

                    // If your API has multiple versions and you've applied the "MultipleApiVersions" setting
                    // as described above, you can also enable a select box in the swagger-ui, that displays
                    // a discovery URL for each version. This provides a convenient way for users to browse documentation
                    // for different API versions.
                    //
                    //c.EnableDiscoveryUrlSelector();

                    // If your API supports the OAuth2 Implicit flow, and you've described it correctly, according to
                    // the Swagger 2.0 specification, you can enable UI support as shown below.
                    //
                    c.EnableOAuth2Support(
                        clientId: "test-client-id",
                        clientSecret: null,
                        realm: "test-realm",
                        appName: "Swagger UI"
                    //additionalQueryStringParams: new Dictionary<string, string>() { { "foo", "bar" } }
                    );

                    // If your API supports ApiKey, you can override the default values.
                    // "apiKeyIn" can either be "query" or "header"                                                
                    //
                    //c.EnableApiKeySupport("apiKey", "header");
                });
        }

        private static string GetXmlCommentsPath(string xmlFileName)
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\{xmlFileName}";
        }

        public static bool ResolveVersionSupportByRouteConstraint(ApiDescription apiDesc, string targetApiVersion)
        {
            var versionConstraint = (apiDesc.Route.Constraints.ContainsKey("apiVersion"))
                ? apiDesc.Route.Constraints["apiVersion"] as RegexRouteConstraint
                : null;

            return (versionConstraint == null)
                ? false
                : versionConstraint.Pattern.Split('|').Contains(targetApiVersion);
        }
    }
}