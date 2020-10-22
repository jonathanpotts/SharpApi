# Get Started

## Creating an API endpoint

To create an API endpoint:

1. Create a class that inherits [`SharpApi.ApiEndpoint`](~/obj/api/SharpApi.ApiEndpoint.yml).
2. Decorate the class with [`SharpApi.Api.ApiEndpointAttribute`](~/obj/api/SharpApi.ApiEndpointAttribute.yml) providing the route to use for the endpoint as well as the accepted HTTP methods.
3. Override the [`RunAsync(ApiRequest)`](~/obj/api/SharpApi.ApiEndpoint.yml#SharpApi_ApiEndpoint_RunAsync_SharpApi_ApiRequest_) method with the code to run when the endpoint is executed.

**Example:**

```cs
using SharpApi;
using System.Threading.Tasks;

[ApiEndpoint("/hello-world", "GET", "HEAD")]
public class HelloWorldEndpoint : ApiEndpoint
{
    public override Task<ApiResult> RunAsync(ApiRequest request)
    {
        return Task.FromResult(new StringResult("Hello, world!"));
    }
}
```

### Receiving the request body as a model

If you are expecting a request body in the form of a JSON-serialized model object, you can instead inherit from [`SharpApi.ApiEndpoint<TModel>`](~/obj/api/SharpApi.ApiEndpoint-1.yml) and specify `TModel` as your model type.

SharpAPI will then attempt to deserialize the request into an object of type `TModel` and provide it as the [`Model`](~/obj/api/SharpApi.ApiRequest-1.yml#SharpApi_ApiRequest_1_Model) property of the `ApiRequest<TModel>` parameter of your API endpoint's [`RunAsync(ApiRequest<TModel>)`](~/obj/api/SharpApi.ApiEndpoint-1.yml#SharpApi_ApiEndpoint_1_RunAsync_SharpApi_ApiRequest__0__) method.

**Example:**

```cs
using SharpApi;
using System.Threading.Tasks;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

[ApiEndpoint("/hello-person", "POST")]
public class HelloPersonEndpoint : ApiEndpoint<Person>
{
    public override Task<ApiResult> RunAsync(ApiRequest<Person> request)
    {
        var name = request.Model.FirstName;

        return Task.FromResult(new StringResult($"Hello, {name}!"));
    }
}
```
