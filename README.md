# ContosoIncAPI

## Structure

The main part of the entire code is split into [`Controllers`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/tree/main/Controllers), [`Entities`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/tree/main/Entities), [`Database.cs`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Database.cs), and [`ApiKeyAttribute.cs`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Security/ApiKeyAttribute.cs).

`Controllers` contains classes with methods that are executed when particular requests are made.

For instance, a GET request to `https://.../api/users/anomalies` will trigger the [`LoginLocationController.GetLoginsFromUnseenCountries()`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Controllers/LoginLocationController.cs#L16) method, which takes no arguments, and returns a JSON string (generated by [Json.NET](https://www.newtonsoft.com/json)) with logins from unseen countries, and some additional data.

`Entities` contains C# record classes for each table from the database, and records for "compound" tables (specifically for tasks 1-3).

For example, [`UnseenCountryConcurrentLogin.cs`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Entities/UnseenCountryConcurrentLogin.cs) is assembled from both [`ConcurrentLogin.cs`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Entities/ConcurrentLogin.cs) (inherits from it) and [`UnseenCountryLogin.cs`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Entities/UnseenCountryLogin.cs) (has it as a property). `ConcurrentLogin.cs`and `UnseenCountryLogin.cs` represent models for concrete tables from the database, whereas `UnseenCountryConcurrentLogin.cs` uses them to form another model, which exists just to satisfy the needs of the request described above.

`Database.cs` comprises of methods for loading data from the database. Each method works exactly with one database table and one non-compound entity.

There is also a private method [`Database.GetReader()`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Database.cs#L17). It is called by every `Database.LoadXXX()` method in order to open a new connection to the database, and returns a `MySQLDataReader` if connection was successfully established; otherwise, returns `null`. This method takes no responsibility for releasing unmanaged code, since finalizers in `MySQLXXX` classes call `Dispose()` implicitly.

[`contoso_inc.sql`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/contoso_inc.sql) is an sql script for all tables in the MySQL database. It is executed on every Docker build.

`ApiKeyAttribute.cs` is a security attribute, used by controllers to guarantee that data will not be accessed, if a correct API key is not provided in a header. The API key is configured in [`appsettings.json`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/appsettings.json) as an `x-api-key` field. All controllers use this attribute.

## How to install

The project can be composed using Docker. Follow these steps:

1. [Download](https://github.com/Aleksandr-Nvk/ContosoIncAPI/archive/refs/heads/main.zip) the files from this repository, or clone it like this:

```gh repo clone Aleksandr-Nvk/ContosoIncAPI```

2. In project files You will find the [`Dockerfile`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/Dockerfile) and the [`docker-compose.yml`](https://github.com/Aleksandr-Nvk/ContosoIncAPI/blob/main/docker-compose.yml) file. Use them both to built images and a docker container:

```docker-compose -f docker-compose.yml up```

3. API is ready for use. Test them with Postman, Swagger, and/or other tool.

## Examples

Examples of GET requests to this API is available in this [Postman workspace](https://www.postman.com/material-geoscientist-84815076/workspace/contosoincapi).
